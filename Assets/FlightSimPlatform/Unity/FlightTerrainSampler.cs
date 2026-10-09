using System;
using CesiumForUnity;
using FlightSim.Platform.Contracts;
using Unity.Mathematics;
using UnityEngine;

namespace FlightSim.Platform.Unity
{
    [Serializable]
    public struct FlightTerrainSample
    {
        public bool IsValid;
        public double SampleTimeS;
        public float AboveGroundLevelM;
        public bool LowAltitudeAlert;
        public bool LandingGearAlert;
        public int ColliderLayer;
    }

    [DisallowMultipleComponent]
    [DefaultExecutionOrder(FlightSimExecutionOrder.TerrainQuery)]
    public sealed class FlightTerrainSampler : MonoBehaviour
    {
        public const string DefaultQueryLayerName = "FlightTerrainQuery";

        [SerializeField] private FlightSimulationHost simulationHost;
        [SerializeField] private Transform aircraftTransform;
        [SerializeField] private string aircraftIdOverride = string.Empty;
        [SerializeField] private CesiumGeoreference georeference;
        [SerializeField] private string terrainQueryLayerName = DefaultQueryLayerName;
        [SerializeField, Min(1f)] private float maximumRayDistanceM = 20000f;
        [SerializeField, Min(0f)] private float rayOriginOffsetM = 1f;
        [SerializeField, Min(0.01f)] private float sampleIntervalS = 0.05f;
        [SerializeField, Min(0f)] private float lowAltitudeAlertHeightM = 120f;
        [SerializeField, Min(0f)] private float landingGearAlertHeightM = 180f;

        private FlightTerrainSample latestSample;
        private double nextSampleTimeS;
        private int terrainLayerMask;
        private bool missingLayerReported;

        public event Action<FlightTerrainSample> SampleUpdated;

        public FlightTerrainSample LatestSample => latestSample;
        public bool HasValidSample => latestSample.IsValid;
        public float AboveGroundLevelM => latestSample.AboveGroundLevelM;
        public float SampleAgeSeconds => latestSample.IsValid
            ? Mathf.Max(0f, (float)(Time.unscaledTimeAsDouble - latestSample.SampleTimeS))
            : float.PositiveInfinity;
        public AircraftId BoundAircraft => string.IsNullOrEmpty(aircraftIdOverride)
            ? simulationHost != null ? simulationHost.LocalAircraft : default(AircraftId)
            : new AircraftId(aircraftIdOverride);

        private void Awake()
        {
            ResolveReferences();
            ResolveTerrainLayer();
        }

        private void Reset()
        {
            ResolveReferences();
            terrainQueryLayerName = DefaultQueryLayerName;
        }

        private void OnValidate()
        {
            maximumRayDistanceM = Mathf.Max(1f, maximumRayDistanceM);
            rayOriginOffsetM = Mathf.Max(0f, rayOriginOffsetM);
            sampleIntervalS = Mathf.Max(0.01f, sampleIntervalS);
            lowAltitudeAlertHeightM = Mathf.Max(0f, lowAltitudeAlertHeightM);
            landingGearAlertHeightM = Mathf.Max(0f, landingGearAlertHeightM);
            ResolveTerrainLayer();
        }

        private void LateUpdate()
        {
            double nowS = Time.unscaledTimeAsDouble;
            if (nowS < nextSampleTimeS)
            {
                return;
            }

            nextSampleTimeS = nowS + sampleIntervalS;
            SampleNow();
        }

        public bool SampleNow()
        {
            if (terrainLayerMask == 0)
            {
                ResolveTerrainLayer();
            }

            if (terrainLayerMask == 0 ||
                simulationHost == null ||
                aircraftTransform == null ||
                georeference == null ||
                !simulationHost.TryGetLatest(BoundAircraft, out AircraftSnapshot snapshot))
            {
                PublishInvalidSample();
                return false;
            }

            Vector3 down = ComputeWorldDown(
                snapshot.Fast.LongitudeRad,
                snapshot.Fast.LatitudeRad);
            if (down.sqrMagnitude < 1e-8f)
            {
                PublishInvalidSample();
                return false;
            }

            down.Normalize();
            Vector3 origin = aircraftTransform.position - down * rayOriginOffsetM;
            if (!Physics.Raycast(
                    origin,
                    down,
                    out RaycastHit hit,
                    maximumRayDistanceM + rayOriginOffsetM,
                    terrainLayerMask,
                    QueryTriggerInteraction.Ignore))
            {
                PublishInvalidSample();
                return false;
            }

            float aglM = Mathf.Max(0f, hit.distance - rayOriginOffsetM);
            LandingGearState gear = snapshot.Systems.LandingGear;
            bool airborne = !gear.WeightOnWheels;
            bool gearDown = gear.NoseGearPositionNormalized >= 0.95 &&
                            gear.LeftMainGearPositionNormalized >= 0.95 &&
                            gear.RightMainGearPositionNormalized >= 0.95;
            latestSample = new FlightTerrainSample
            {
                IsValid = true,
                SampleTimeS = Time.unscaledTimeAsDouble,
                AboveGroundLevelM = aglM,
                LowAltitudeAlert = airborne && aglM < lowAltitudeAlertHeightM,
                LandingGearAlert = airborne && !gearDown && aglM < landingGearAlertHeightM,
                ColliderLayer = hit.collider.gameObject.layer
            };
            simulationHost.SetTerrainSample(BoundAircraft, true, aglM, 0.0,
                hit.collider.GetComponent<CachedTerrainSurface>() != null ? TerrainSource.MissionCache : TerrainSource.Cesium);
            SampleUpdated?.Invoke(latestSample);
            return true;
        }

        private void ResolveReferences()
        {
            if (simulationHost == null)
            {
                simulationHost = FindObjectOfType<FlightSimulationHost>();
            }

            if (aircraftTransform == null)
            {
                CesiumAircraftView view = GetComponent<CesiumAircraftView>();
                aircraftTransform = view != null ? view.transform : transform;
            }

            if (georeference == null)
            {
                georeference = aircraftTransform != null
                    ? aircraftTransform.GetComponentInParent<CesiumGeoreference>()
                    : GetComponentInParent<CesiumGeoreference>();
            }
        }

        public void Bind(FlightSimulationHost host, Transform aircraft, AircraftId aircraftId)
        {
            simulationHost = host;
            aircraftTransform = aircraft != null ? aircraft : transform;
            aircraftIdOverride = aircraftId.Value;
            ResolveReferences();
        }

        private void ResolveTerrainLayer()
        {
            int layer = LayerMask.NameToLayer(terrainQueryLayerName);
            terrainLayerMask = layer >= 0 ? 1 << layer : 0;
            if (terrainLayerMask == 0 && !missingLayerReported && Application.isPlaying)
            {
                Debug.LogWarning(
                    $"FlightTerrainSampler is disabled until the dedicated '{terrainQueryLayerName}' layer exists.",
                    this);
                missingLayerReported = true;
            }
        }

        private Vector3 ComputeWorldDown(double longitudeRad, double latitudeRad)
        {
            double cosLatitude = Math.Cos(latitudeRad);
            double3 downEcef = new double3(
                -cosLatitude * Math.Cos(longitudeRad),
                -cosLatitude * Math.Sin(longitudeRad),
                -Math.Sin(latitudeRad));
            double3 georeferenceLocalDown =
                georeference.TransformEarthCenteredEarthFixedDirectionToUnity(downEcef);
            Vector3 localDown = new Vector3(
                (float)georeferenceLocalDown.x,
                (float)georeferenceLocalDown.y,
                (float)georeferenceLocalDown.z);
            return georeference.transform.TransformDirection(localDown);
        }

        private void PublishInvalidSample()
        {
            latestSample = new FlightTerrainSample
            {
                IsValid = false,
                SampleTimeS = Time.unscaledTimeAsDouble,
                AboveGroundLevelM = float.PositiveInfinity,
                LowAltitudeAlert = false,
                LandingGearAlert = false,
                ColliderLayer = -1
            };
            if (simulationHost != null)
            {
                simulationHost.SetTerrainSample(BoundAircraft, false, 0.0, 0.0);
            }

            SampleUpdated?.Invoke(latestSample);
        }
    }
}

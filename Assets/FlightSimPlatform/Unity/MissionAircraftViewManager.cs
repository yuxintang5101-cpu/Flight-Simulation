using System;
using System.Collections.Generic;
using CesiumForUnity;
using FlightSim.Platform.Contracts;
using UnityEngine;

namespace FlightSim.Platform.Unity
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(FlightSimExecutionOrder.AircraftVisual - 10)]
    public sealed class MissionAircraftViewManager : MonoBehaviour
    {
        public const int MaximumViews = 8;

        [SerializeField] private FlightSimulationHost simulationHost;
        [SerializeField] private CesiumAircraftView playerView;
        [SerializeField] private GameObject aircraftVisualPrefab;
        [SerializeField] private bool showDebugLabels;
        [SerializeField] private Color friendlyTint = new Color(0.78f, 0.9f, 1f, 1f);
        [SerializeField] private Color hostileTint = new Color(1f, 0.48f, 0.38f, 1f);

        private readonly List<CesiumAircraftView> spawnedViews = new List<CesiumAircraftView>(MaximumViews - 1);

        public int SpawnedViewCount => spawnedViews.Count;

        private void OnEnable()
        {
            ResolveReferences();
            if (simulationHost != null)
                simulationHost.MissionLoaded += OnMissionLoaded;
            SyncRoster();
        }

        private void Start()
        {
            SyncRoster();
        }

        private void OnDisable()
        {
            if (simulationHost != null)
                simulationHost.MissionLoaded -= OnMissionLoaded;
            ClearSpawnedViews();
        }

        public void Configure(
            FlightSimulationHost host,
            CesiumAircraftView localPlayerView,
            GameObject visualPrefab)
        {
            if (simulationHost != null)
                simulationHost.MissionLoaded -= OnMissionLoaded;
            simulationHost = host;
            playerView = localPlayerView;
            aircraftVisualPrefab = visualPrefab;
            if (isActiveAndEnabled && simulationHost != null)
                simulationHost.MissionLoaded += OnMissionLoaded;
            SyncRoster();
        }

        public void SyncRoster()
        {
            ClearSpawnedViews();
            if (simulationHost == null) return;
            if (playerView != null) playerView.Bind(simulationHost, simulationHost.LocalAircraft);
            if (simulationHost.LoadedMission == null) return;
            MissionActorDefinition[] actors = simulationHost.LoadedMission.Actors ?? Array.Empty<MissionActorDefinition>();
            for (int index = 0; index < actors.Length && index < MaximumViews; index++)
            {
                MissionActorDefinition actor = actors[index];
                AircraftId aircraft = new AircraftId(actor.AircraftId);
                if (actor.IsPlayer)
                {
                    if (playerView != null)
                        playerView.Bind(simulationHost, aircraft);
                    continue;
                }

                CreateActorView(in actor, aircraft);
            }
        }

        private void CreateActorView(in MissionActorDefinition actor, AircraftId aircraft)
        {
            if (aircraftVisualPrefab == null)
                return;

            GameObject root = new GameObject($"MissionAircraft_{actor.AircraftId}");
            root.transform.SetParent(playerView != null ? playerView.transform.parent : transform, false);
            CesiumGlobeAnchor anchor = root.AddComponent<CesiumGlobeAnchor>();
            anchor.detectTransformChanges = false;
            anchor.adjustOrientationForGlobeWhenMoving = false;
            CesiumAircraftView view = root.AddComponent<CesiumAircraftView>();
            view.Bind(simulationHost, aircraft);

            GameObject visual = Instantiate(aircraftVisualPrefab, root.transform, false);
            visual.name = "F35_Visual";
            Transform cockpit = FindDeepChild(visual.transform, "F35_Cockpit");
            if (cockpit != null)
                cockpit.gameObject.SetActive(false);
            ApplyFactionTint(visual, actor.Side);
            if (showDebugLabels)
                AddDebugLabel(root.transform, actor.Callsign, actor.Side);
            spawnedViews.Add(view);
        }

        private void ApplyFactionTint(GameObject visual, AircraftSide side)
        {
            if (side == AircraftSide.Neutral)
                return;

            Color tint = side == AircraftSide.Hostile ? hostileTint : friendlyTint;
            Renderer[] renderers = visual.GetComponentsInChildren<Renderer>(true);
            var block = new MaterialPropertyBlock();
            for (int index = 0; index < renderers.Length; index++)
            {
                Renderer renderer = renderers[index];
                renderer.GetPropertyBlock(block);
                block.SetColor("_Color", tint);
                renderer.SetPropertyBlock(block);
            }
        }

        private static void AddDebugLabel(Transform parent, string callsign, AircraftSide side)
        {
            GameObject labelObject = new GameObject("Debug Callsign");
            labelObject.transform.SetParent(parent, false);
            labelObject.transform.localPosition = new Vector3(0f, 4f, 0f);
            TextMesh label = labelObject.AddComponent<TextMesh>();
            label.text = string.IsNullOrEmpty(callsign) ? parent.name : callsign;
            label.anchor = TextAnchor.MiddleCenter;
            label.alignment = TextAlignment.Center;
            label.characterSize = 0.5f;
            label.fontSize = 28;
            label.color = side == AircraftSide.Hostile ? Color.red : Color.cyan;
        }

        private void OnMissionLoaded(MissionDefinition mission)
        {
            SyncRoster();
        }

        private void ResolveReferences()
        {
            if (simulationHost == null)
                simulationHost = FindObjectOfType<FlightSimulationHost>();
            if (playerView == null)
            {
                CesiumAircraftView[] views = FindObjectsOfType<CesiumAircraftView>();
                for (int index = 0; index < views.Length; index++)
                {
                    if (views[index].gameObject.name.StartsWith("Player_Aircraft", StringComparison.Ordinal))
                    {
                        playerView = views[index];
                        break;
                    }
                }
            }
        }

        private void ClearSpawnedViews()
        {
            for (int index = 0; index < spawnedViews.Count; index++)
            {
                CesiumAircraftView view = spawnedViews[index];
                if (view == null) continue;
                if (Application.isPlaying) Destroy(view.gameObject);
                else DestroyImmediate(view.gameObject);
            }
            spawnedViews.Clear();
        }

        private static Transform FindDeepChild(Transform parent, string name)
        {
            if (parent.name == name) return parent;
            for (int index = 0; index < parent.childCount; index++)
            {
                Transform found = FindDeepChild(parent.GetChild(index), name);
                if (found != null) return found;
            }
            return null;
        }
    }
}

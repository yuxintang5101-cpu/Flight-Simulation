using System;
using CesiumForUnity;
using UnityEngine;

namespace FlightSim.Platform.Unity
{
    public enum FlightCameraMode
    {
        Chase,
        Cockpit,
        Free
    }

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(CesiumGlobeAnchor))]
    [RequireComponent(typeof(CesiumOriginShift))]
    [DefaultExecutionOrder(FlightSimExecutionOrder.Camera)]
    public sealed class FlightCameraRig : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private Transform aircraftTarget;
        [SerializeField] private Transform cockpitEye;
        [SerializeField] private Transform exteriorModelRoot;
        [SerializeField] private Transform cockpitModelRoot;

        [Header("Mode")]
        [SerializeField] private FlightCameraMode mode = FlightCameraMode.Chase;
        [SerializeField] private bool hideExteriorInCockpit = true;
        [SerializeField] private bool hideCockpitOutsideCockpit = true;

        [Header("Chase")]
        [SerializeField] private Vector3 chaseOffset = new Vector3(0f, 4.2f, -14f);
        [SerializeField] private Vector3 chaseLookAtOffset = new Vector3(0f, 1.2f, 4.5f);
        [SerializeField, Min(0.01f)] private float chasePositionSharpness = 5.5f;
        [SerializeField, Min(0.01f)] private float chaseRotationSharpness = 7f;

        [Header("Cockpit Fallback")]
        [SerializeField] private Vector3 cockpitLocalPosition = new Vector3(0f, 1.05f, 1.65f);
        [SerializeField] private Vector3 cockpitLocalEuler = new Vector3(8f, 0f, 0f);

        [Header("Free Orbit")]
        [SerializeField, Range(-89f, 89f)] private float freeOrbitPitch = 12f;
        [SerializeField] private float freeOrbitYaw;
        [SerializeField, Min(1f)] private float freeOrbitDistance = 22f;
        [SerializeField, Min(1f)] private float freeOrbitSensitivity = 110f;
        [SerializeField, Min(1f)] private float freeOrbitMinimumDistance = 7f;
        [SerializeField, Min(1f)] private float freeOrbitMaximumDistance = 80f;

        private FlightSimulationHost sessionHost;
        private CesiumGlobeAnchor cameraAnchor;
        private FlightCameraMode? appliedVisibilityMode;

        public event Action<FlightCameraMode> ModeChanged;
        public event Action<bool> CockpitActiveChanged;

        public FlightCameraMode Mode => mode;
        public bool IsCockpitActive => mode == FlightCameraMode.Cockpit;
        public Transform AircraftTarget => aircraftTarget;

        private void Awake()
        {
            sessionHost = FindObjectOfType<FlightSimulationHost>();
            cameraAnchor = GetComponent<CesiumGlobeAnchor>();
            ConfigureCameraAnchor();

            if (!CompareTag("MainCamera"))
            {
                Debug.LogError("FlightCameraRig must be placed on the Main Camera.", this);
            }

            if (aircraftTarget != null && transform.IsChildOf(aircraftTarget))
            {
                Debug.LogError(
                    "The Main Camera must be a sibling of the aircraft under the CesiumGeoreference, not a child of the aircraft.",
                    this);
            }
        }

        private void OnEnable()
        {
            ApplyModelVisibility(true);
        }

        private void Reset()
        {
            gameObject.tag = "MainCamera";
            cameraAnchor = GetComponent<CesiumGlobeAnchor>();
            ConfigureCameraAnchor();
        }

        private void OnValidate()
        {
            chasePositionSharpness = Mathf.Max(0.01f, chasePositionSharpness);
            chaseRotationSharpness = Mathf.Max(0.01f, chaseRotationSharpness);
            freeOrbitDistance = Mathf.Max(1f, freeOrbitDistance);
            freeOrbitMinimumDistance = Mathf.Max(1f, freeOrbitMinimumDistance);
            freeOrbitMaximumDistance = Mathf.Max(freeOrbitMinimumDistance, freeOrbitMaximumDistance);
            cameraAnchor = GetComponent<CesiumGlobeAnchor>();
            ConfigureCameraAnchor();
        }

        private void Update()
        {
            if ((sessionHost != null && (sessionHost.IsSessionPaused || sessionHost.IsDisplayInputCaptured)) || mode != FlightCameraMode.Free)
            {
                return;
            }

            freeOrbitYaw += Input.GetAxisRaw("Mouse X") * freeOrbitSensitivity * Time.deltaTime;
            freeOrbitPitch -= Input.GetAxisRaw("Mouse Y") * freeOrbitSensitivity * Time.deltaTime;
            freeOrbitPitch = Mathf.Clamp(freeOrbitPitch, -89f, 89f);
            freeOrbitDistance = Mathf.Clamp(
                freeOrbitDistance - Input.mouseScrollDelta.y * 3f,
                freeOrbitMinimumDistance,
                freeOrbitMaximumDistance);
        }

        private void LateUpdate()
        {
            if (aircraftTarget == null)
            {
                return;
            }

            ApplyModelVisibility(false);
            switch (mode)
            {
                case FlightCameraMode.Cockpit:
                    UpdateCockpitCamera();
                    break;
                case FlightCameraMode.Free:
                    UpdateFreeCamera();
                    break;
                default:
                    UpdateChaseCamera();
                    break;
            }

            cameraAnchor.Sync();
        }

        public void SetMode(FlightCameraMode newMode)
        {
            if (mode == newMode)
            {
                return;
            }

            bool wasCockpitActive = IsCockpitActive;
            mode = newMode;
            ApplyModelVisibility(true);
            ModeChanged?.Invoke(mode);
            if (wasCockpitActive != IsCockpitActive)
            {
                CockpitActiveChanged?.Invoke(IsCockpitActive);
            }
        }

        public void CycleMode()
        {
            FlightCameraMode next = mode == FlightCameraMode.Chase
                ? FlightCameraMode.Cockpit
                : mode == FlightCameraMode.Cockpit
                    ? FlightCameraMode.Free
                    : FlightCameraMode.Chase;
            SetMode(next);
        }

        private void UpdateChaseCamera()
        {
            Vector3 desiredPosition = aircraftTarget.TransformPoint(chaseOffset);
            Vector3 lookAt = aircraftTarget.TransformPoint(chaseLookAtOffset);
            float positionT = 1f - Mathf.Exp(-chasePositionSharpness * Time.deltaTime);
            float rotationT = 1f - Mathf.Exp(-chaseRotationSharpness * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, positionT);

            Vector3 lookDirection = lookAt - transform.position;
            if (lookDirection.sqrMagnitude > 1e-8f)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(lookDirection, aircraftTarget.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationT);
            }
        }

        private void UpdateCockpitCamera()
        {
            if (cockpitEye != null)
            {
                transform.SetPositionAndRotation(cockpitEye.position, cockpitEye.rotation);
                return;
            }

            transform.SetPositionAndRotation(
                aircraftTarget.TransformPoint(cockpitLocalPosition),
                aircraftTarget.rotation * Quaternion.Euler(cockpitLocalEuler));
        }

        private void UpdateFreeCamera()
        {
            Vector3 lookAt = aircraftTarget.TransformPoint(chaseLookAtOffset);
            Quaternion orbit = aircraftTarget.rotation * Quaternion.Euler(freeOrbitPitch, freeOrbitYaw, 0f);
            Vector3 desiredPosition = lookAt + orbit * Vector3.back * freeOrbitDistance;
            float positionT = 1f - Mathf.Exp(-7f * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, positionT);

            Vector3 lookDirection = lookAt - transform.position;
            if (lookDirection.sqrMagnitude > 1e-8f)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection, aircraftTarget.up);
            }
        }

        private void ApplyModelVisibility(bool force)
        {
            if (!force && appliedVisibilityMode.HasValue && appliedVisibilityMode.Value == mode)
            {
                return;
            }

            bool cockpitActive = IsCockpitActive;
            if (exteriorModelRoot != null && hideExteriorInCockpit)
            {
                exteriorModelRoot.gameObject.SetActive(!cockpitActive);
            }

            if (cockpitModelRoot != null && hideCockpitOutsideCockpit)
            {
                cockpitModelRoot.gameObject.SetActive(cockpitActive);
            }

            appliedVisibilityMode = mode;
        }

        private void ConfigureCameraAnchor()
        {
            if (cameraAnchor == null)
            {
                return;
            }

            cameraAnchor.detectTransformChanges = false;
            cameraAnchor.adjustOrientationForGlobeWhenMoving = false;
        }
    }
}

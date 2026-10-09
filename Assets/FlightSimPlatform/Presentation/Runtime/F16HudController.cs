using FlightSim.Platform.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlightSim.Platform.Presentation
{
    [DisallowMultipleComponent]
    public sealed class F16HudController : MonoBehaviour
    {
        private const int VectorCapacity = 1024;
        private const int LabelCapacity = 96;
        private const string RuntimeRootName = "F-16C HUD Runtime";
        private const string FontResourcePath = "Fonts/F16 HUD Font";

        [Header("HUD Appearance")]
        [SerializeField]
        private Color primaryColor = new Color(0.18f, 1f, 0.43f, 0.96f);

        [SerializeField]
        private Color dimColor = new Color(0.14f, 0.82f, 0.34f, 0.68f);

        [SerializeField]
        private Color warningColor = new Color(1f, 0.18f, 0.08f, 1f);

        [SerializeField]
        private Color cautionColor = new Color(1f, 0.72f, 0.08f, 1f);

        [SerializeField, Min(0.75f)]
        private float lineWidth = 2f;

        [SerializeField]
        private int sortingOrder = 100;

        [SerializeField, Range(0.80f, 1.50f)]
        private float hudScale = 1.20f;

        private GameObject _generatedRoot;
        private Canvas _canvas;
        private RectTransform _canvasRect;
        private RectTransform _combinerRect;
        private HudVectorGraphic _vectorGraphic;
        private HudVectorCommandBuffer _vectors;
        private HudLabelPool _labels;
        private TMP_FontAsset _fontAsset;
        private HudState _state;
        private AircraftCombatState _combatState;
        private Camera _cockpitCamera;
        private bool _cockpitActive;
        private bool _lastCameraUsable;
        private Vector2 _lastCanvasSize;
        private Rect _safeRect;

        public bool IsHudVisible => _generatedRoot != null && _generatedRoot.activeSelf;
        public float HudScale => hudScale;

        public void SetCockpitContext(bool cockpitActive, Camera cockpitCamera)
        {
            _cockpitActive = cockpitActive;
            _cockpitCamera = cockpitCamera;
            EnsureHierarchy();
            _canvas.worldCamera = cockpitCamera;
            Refresh();
        }

        public void SetHudState(HudState state)
        {
            _state = state;
            EnsureHierarchy();
            Refresh();
        }

        public void SetCombatState(AircraftCombatState state)
        {
            _combatState = state;
            EnsureHierarchy();
            Refresh();
        }

        private void OnValidate()
        {
            hudScale = Mathf.Clamp(hudScale, 0.80f, 1.50f);
            lineWidth = Mathf.Max(0.75f, lineWidth);
            if (_generatedRoot != null)
                Refresh();
        }

        private void Awake()
        {
            EnsureHierarchy();
            Refresh();
        }

        private void OnEnable()
        {
            EnsureHierarchy();
            Refresh();
        }

        private void LateUpdate()
        {
            if (_generatedRoot == null)
                return;

            bool cameraUsable = IsCameraUsable(_cockpitCamera);
            Vector2 canvasSize = ReadCanvasSize();
            if (cameraUsable != _lastCameraUsable || canvasSize != _lastCanvasSize)
                Refresh();
        }

        private void OnDisable()
        {
            if (_labels != null)
                _labels.HideAll();
            if (_generatedRoot != null)
                _generatedRoot.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_generatedRoot != null)
                DestroyOwnedObject(_generatedRoot);
            _generatedRoot = null;
            _fontAsset = null;
        }

        private void EnsureHierarchy()
        {
            if (_generatedRoot != null)
                return;

            _generatedRoot = new GameObject(
                RuntimeRootName,
                typeof(RectTransform),
                typeof(Canvas),
                typeof(CanvasScaler));
            _generatedRoot.hideFlags = HideFlags.DontSave;
            _generatedRoot.transform.SetParent(transform, false);

            _canvasRect = _generatedRoot.GetComponent<RectTransform>();
            _canvas = _generatedRoot.GetComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = _cockpitCamera;
            _canvas.planeDistance = 0.5f;
            _canvas.pixelPerfect = false;
            _canvas.overrideSorting = true;
            _canvas.sortingOrder = sortingOrder;

            CanvasScaler scaler = _generatedRoot.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(
                F16HudLayoutMath.ReferenceWidth,
                F16HudLayoutMath.ReferenceHeight);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 1f;
            scaler.referencePixelsPerUnit = 100f;

            var combinerObject = new GameObject("Combiner Safe Area", typeof(RectTransform), typeof(RectMask2D));
            combinerObject.hideFlags = HideFlags.DontSave;
            _combinerRect = combinerObject.GetComponent<RectTransform>();
            _combinerRect.SetParent(_canvasRect, false);
            _combinerRect.anchorMin = new Vector2(0.5f, 0.5f);
            _combinerRect.anchorMax = new Vector2(0.5f, 0.5f);
            _combinerRect.pivot = new Vector2(0.5f, 0.5f);
            _combinerRect.anchoredPosition = new Vector2(
                0f,
                F16HudLayoutMath.CombinerVerticalOffset);
            _combinerRect.localScale = new Vector3(
                F16HudLayoutMath.CombinerRenderedWidth / F16HudLayoutMath.CombinerWidth * hudScale,
                F16HudLayoutMath.CombinerRenderedHeight / F16HudLayoutMath.CombinerHeight * hudScale,
                1f);

            var vectorObject = new GameObject("HUD Vector Mesh", typeof(RectTransform), typeof(HudVectorGraphic));
            vectorObject.hideFlags = HideFlags.DontSave;
            RectTransform vectorRect = vectorObject.GetComponent<RectTransform>();
            vectorRect.SetParent(_combinerRect, false);
            StretchToParent(vectorRect);

            _vectors = new HudVectorCommandBuffer(VectorCapacity);
            _vectorGraphic = vectorObject.GetComponent<HudVectorGraphic>();
            _vectorGraphic.color = Color.white;
            _vectorGraphic.raycastTarget = false;
            _vectorGraphic.maskable = true;
            _vectorGraphic.SetCommands(_vectors);

            _fontAsset = Resources.Load<TMP_FontAsset>(FontResourcePath);
            _labels = new HudLabelPool(_combinerRect, LabelCapacity, _fontAsset);

            UpdateLayoutSize();
        }

        private void Refresh()
        {
            if (_generatedRoot == null)
                return;

            bool cameraUsable = IsCameraUsable(_cockpitCamera);
            bool visible = isActiveAndEnabled &&
                           gameObject.activeInHierarchy &&
                           _cockpitActive &&
                           cameraUsable &&
                           F16HudLayoutMath.IsDisplayable(_state);

            _lastCameraUsable = cameraUsable;
            _canvas.worldCamera = _cockpitCamera;

            if (!visible)
            {
                _labels.HideAll();
                _generatedRoot.SetActive(false);
                return;
            }

            if (!_generatedRoot.activeSelf)
                _generatedRoot.SetActive(true);

            UpdateLayoutSize();
            var style = new F16HudStyle(primaryColor, dimColor, warningColor, cautionColor, lineWidth);
            F16HudComposer.Compose(_state, _combatState, _safeRect, _vectors, _labels, style);
            _vectorGraphic.SetCommands(_vectors);
        }

        private void UpdateLayoutSize()
        {
            Vector2 canvasSize = ReadCanvasSize();
            _lastCanvasSize = canvasSize;
            _safeRect = F16HudLayoutMath.CalculateSafeRect(canvasSize.x, canvasSize.y);
            _combinerRect.sizeDelta = _safeRect.size;
            _combinerRect.anchoredPosition = new Vector2(
                0f,
                F16HudLayoutMath.CombinerVerticalOffset);
            _combinerRect.localScale = new Vector3(
                F16HudLayoutMath.CombinerRenderedWidth / F16HudLayoutMath.CombinerWidth * hudScale,
                F16HudLayoutMath.CombinerRenderedHeight / F16HudLayoutMath.CombinerHeight * hudScale,
                1f);
        }

        private Vector2 ReadCanvasSize()
        {
            if (_canvasRect == null)
                return new Vector2(F16HudLayoutMath.ReferenceWidth, F16HudLayoutMath.ReferenceHeight);

            Vector2 size = _canvasRect.rect.size;
            if (size.x < 1f || size.y < 1f)
                return new Vector2(F16HudLayoutMath.ReferenceWidth, F16HudLayoutMath.ReferenceHeight);
            return size;
        }

        private static bool IsCameraUsable(Camera camera)
        {
            return camera != null && camera.isActiveAndEnabled;
        }

        private static void StretchToParent(RectTransform rect)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = Vector2.zero;
        }

        private static void DestroyOwnedObject(Object ownedObject)
        {
            if (ownedObject == null)
                return;

            if (Application.isPlaying)
                Destroy(ownedObject);
            else
                DestroyImmediate(ownedObject);
        }
    }
}

using TMPro;
using UnityEngine;

namespace FlightSim.Platform.Presentation
{
    internal sealed class HudLabelPool
    {
        private readonly TextMeshProUGUI[] _labels;
        private int _usedCount;
        private int _activeCount;

        public int Capacity => _labels.Length;
        public int UsedCount => _usedCount;
        public bool Overflowed { get; private set; }

        public HudLabelPool(RectTransform parent, int capacity, TMP_FontAsset fontAsset)
        {
            _labels = new TextMeshProUGUI[Mathf.Max(1, capacity)];
            for (int i = 0; i < _labels.Length; i++)
            {
                var labelObject = new GameObject("HudLabel " + i.ToString("00"), typeof(RectTransform));
                labelObject.SetActive(false);
                RectTransform rect = labelObject.GetComponent<RectTransform>();
                rect.SetParent(parent, false);
                rect.anchorMin = new Vector2(0.5f, 0.5f);
                rect.anchorMax = new Vector2(0.5f, 0.5f);
                rect.pivot = new Vector2(0.5f, 0.5f);

                TextMeshProUGUI label = labelObject.AddComponent<TextMeshProUGUI>();
                label.font = fontAsset;
                label.alignment = TextAlignmentOptions.Center;
                label.enableAutoSizing = false;
                label.enableWordWrapping = false;
                label.overflowMode = TextOverflowModes.Truncate;
                label.richText = false;
                label.parseCtrlCharacters = false;
                label.raycastTarget = false;
                label.text = string.Empty;
                _labels[i] = label;
            }
        }

        public void BeginFrame()
        {
            _usedCount = 0;
            Overflowed = false;
        }

        public TextMeshProUGUI Acquire(
            string text,
            Vector2 position,
            Vector2 size,
            float fontSize,
            Color color,
            TextAlignmentOptions alignment = TextAlignmentOptions.Center)
        {
            TextMeshProUGUI label = Prepare(position, size, fontSize, color, alignment);
            if (label == null)
                return null;

            label.text = text ?? string.Empty;
            label.gameObject.SetActive(true);
            return label;
        }

        public TextMeshProUGUI AcquireNumber(
            string format,
            float value,
            Vector2 position,
            Vector2 size,
            float fontSize,
            Color color,
            TextAlignmentOptions alignment = TextAlignmentOptions.Center)
        {
            TextMeshProUGUI label = Prepare(position, size, fontSize, color, alignment);
            if (label == null)
                return null;

            label.SetText(format, value);
            label.gameObject.SetActive(true);
            return label;
        }

        private TextMeshProUGUI Prepare(
            Vector2 position,
            Vector2 size,
            float fontSize,
            Color color,
            TextAlignmentOptions alignment)
        {
            if (_usedCount >= _labels.Length)
            {
                Overflowed = true;
                return null;
            }

            TextMeshProUGUI label = _labels[_usedCount++];
            RectTransform rect = label.rectTransform;
            rect.anchoredPosition = position;
            rect.sizeDelta = size;
            label.fontSize = fontSize;
            label.color = color;
            label.alignment = alignment;
            return label;
        }

        public void EndFrame()
        {
            for (int i = _usedCount; i < _activeCount; i++)
                _labels[i].gameObject.SetActive(false);
            _activeCount = _usedCount;
        }

        public void HideAll()
        {
            for (int i = 0; i < _activeCount; i++)
                _labels[i].gameObject.SetActive(false);
            _usedCount = 0;
            _activeCount = 0;
            Overflowed = false;
        }
    }
}

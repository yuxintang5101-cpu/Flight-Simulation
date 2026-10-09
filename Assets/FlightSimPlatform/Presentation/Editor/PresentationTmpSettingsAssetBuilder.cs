using TMPro;
using UnityEditor;
using UnityEngine;

namespace FlightSim.Platform.Presentation.Editor
{
    public static class PresentationTmpSettingsAssetBuilder
    {
        private const string ResourcesFolder =
            "Assets/FlightSimPlatform/Presentation/Resources";
        private const string AssetPath = ResourcesFolder + "/TMP Settings.asset";
        private const string FontAssetPath = ResourcesFolder + "/Fonts/F16 HUD Font.asset";

        public static void Create()
        {
            if (!AssetDatabase.IsValidFolder(ResourcesFolder))
            {
                AssetDatabase.CreateFolder(
                    "Assets/FlightSimPlatform/Presentation",
                    "Resources");
            }

            TMP_FontAsset fontAsset = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(FontAssetPath);
            if (fontAsset == null)
                throw new System.InvalidOperationException("F-16 HUD TMP font asset did not import.");
            TMP_Settings settings = AssetDatabase.LoadAssetAtPath<TMP_Settings>(AssetPath);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<TMP_Settings>();
                AssetDatabase.CreateAsset(settings, AssetPath);
            }

            var serialized = new SerializedObject(settings);
            serialized.FindProperty("m_enableWordWrapping").boolValue = false;
            serialized.FindProperty("m_enableKerning").boolValue = true;
            serialized.FindProperty("m_enableExtraPadding").boolValue = false;
            serialized.FindProperty("m_enableParseEscapeCharacters").boolValue = false;
            serialized.FindProperty("m_EnableRaycastTarget").boolValue = false;
            serialized.FindProperty("m_GetFontFeaturesAtRuntime").boolValue = true;
            serialized.FindProperty("m_defaultFontSize").floatValue = 36f;
            serialized.FindProperty("m_defaultFontAsset").objectReferenceValue = fontAsset;
            serialized.FindProperty("m_defaultFontAssetPath").stringValue = "Fonts/";
            serialized.FindProperty("m_defaultAutoSizeMinRatio").floatValue = 0.5f;
            serialized.FindProperty("m_defaultAutoSizeMaxRatio").floatValue = 2f;
            serialized.FindProperty("m_defaultTextMeshProTextContainerSize").vector2Value = new Vector2(20f, 5f);
            serialized.FindProperty("m_defaultTextMeshProUITextContainerSize").vector2Value = new Vector2(200f, 50f);
            serialized.ApplyModifiedPropertiesWithoutUndo();

            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();
        }
    }
}

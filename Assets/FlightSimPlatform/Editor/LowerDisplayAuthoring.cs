using System;
using System.IO;
using System.Linq;
using FlightSim.Platform.Presentation.LowerDisplay;
using UnityEditor;
using UnityEngine;

namespace FlightSim.Platform.Editor
{
    public static class LowerDisplayAuthoring
    {
        [MenuItem("FlightSim/Lower Display/Rebuild Surface Binding")]
        public static void Install()
        {
            EnsureLayer("FlightMfd");EnsureLayer("FlightMfdOwnAircraft");
            const string path="Assets/FlightSimPlatform/Presentation/Resources/LowerDisplay/Surface.asset";
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            var prefab=AssetDatabase.LoadAssetAtPath<GameObject>("Assets/FlightSimPlatform/Prefabs/F35_Visual.prefab");
            if(prefab==null)throw new InvalidOperationException("F35 visual prefab missing.");
            var renderer=prefab.GetComponentsInChildren<MeshRenderer>(true).FirstOrDefault(r=>AnimationUtility.CalculateTransformPath(r.transform,prefab.transform).StartsWith("F35_Cockpit/")&&r.sharedMaterials.Any(m=>m!=null&&m.name=="f35_mfd"));
            if(renderer==null)throw new InvalidOperationException("Central f35_mfd renderer missing.");
            int sub=Array.FindIndex(renderer.sharedMaterials,m=>m!=null&&m.name=="f35_mfd");var mesh=renderer.GetComponent<MeshFilter>().sharedMesh;var indices=mesh.GetTriangles(sub).Distinct().ToArray();
            if(indices.Length!=4)throw new InvalidOperationException("Expected a four-corner screen. Update the surface profile for this model.");
            var asset=AssetDatabase.LoadAssetAtPath<LowerDisplaySurfaceProfile>(path);bool create=asset==null;if(create)asset=ScriptableObject.CreateInstance<LowerDisplaySurfaceProfile>();
            asset.VisualRootName="F35_Visual";asset.RendererPath=AnimationUtility.CalculateTransformPath(renderer.transform,prefab.transform);asset.MaterialIndex=sub;
            foreach(int i in indices){Vector2 uv=mesh.uv[i];Vector3 point=prefab.transform.InverseTransformPoint(renderer.transform.TransformPoint(mesh.vertices[i]));if(uv.x<.5f){if(uv.y<.5f)asset.BottomLeft=point;else asset.TopLeft=point;}else{if(uv.y<.5f)asset.BottomRight=point;else asset.TopRight=point;}}
            if(create)AssetDatabase.CreateAsset(asset,path);else EditorUtility.SetDirty(asset);AssetDatabase.SaveAssets();
            Debug.Log("Lower Display installed: 2048 x 1024 native renderer, baked cockpit surface, independent HOTAS display bindings.");
        }
        private static void EnsureLayer(string name)
        {
            if(LayerMask.NameToLayer(name)>=0)return;var obj=new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);var layers=obj.FindProperty("layers");
            for(int i=31;i>=8;i--){var entry=layers.GetArrayElementAtIndex(i);if(!string.IsNullOrEmpty(entry.stringValue))continue;entry.stringValue=name;obj.ApplyModifiedProperties();return;}
            throw new InvalidOperationException("No free layer for "+name);
        }
    }
}

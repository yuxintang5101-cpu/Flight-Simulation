using System;
using System.IO;
using FlightSim.Platform.Missions;
using FlightSim.Platform.Unity;
using UnityEditor;
using UnityEngine;

namespace FlightSim.Platform.Editor
{
    public static class FlightFrameworkAuthoring
    {
        public const string ResourceDirectory = "Assets/FlightSimPlatform/Samples/Resources/FlightSim";
        [MenuItem("FlightSim/Framework/Install or Update Foundation Assets")]
        public static void Install()
        {
            Directory.CreateDirectory(ResourceDirectory);
            Directory.CreateDirectory("Assets/FlightSimPlatform/Samples/Missions");
            AssetDatabase.Refresh();
            File.Copy("Assets/FlightSimPlatform/Missions/Terrain/KTEX_TerrainCache.json",ResourceDirectory+"/KTEX_TerrainCache.json",true);
            string missionPath="Assets/FlightSimPlatform/Samples/Missions/TrainingNavigation.asset";
            var mission=AssetDatabase.LoadAssetAtPath<MissionDefinitionAsset>(missionPath);
            if(mission==null){mission=ScriptableObject.CreateInstance<MissionDefinitionAsset>();mission.SetDefinition(TrainingMissionFactory.CreateNavigationTraining());AssetDatabase.CreateAsset(mission,missionPath);}
            string libraryPath=ResourceDirectory+"/MissionLibrary.asset";
            var library=AssetDatabase.LoadAssetAtPath<FlightMissionLibrary>(libraryPath);
            if(library==null){library=ScriptableObject.CreateInstance<FlightMissionLibrary>();library.Missions=new[]{mission};AssetDatabase.CreateAsset(library,libraryPath);}
            string effectsPath=ResourceDirectory+"/DefaultEffects.asset";
            if(AssetDatabase.LoadAssetAtPath<FlightEffectProfile>(effectsPath)==null) AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<FlightEffectProfile>(),effectsPath);
            Directory.CreateDirectory("Assets/StreamingAssets/FlightSim/Missions");
            AssetDatabase.SaveAssets();AssetDatabase.Refresh();
            Debug.Log("FlightSim foundation ready: mission library, navigation template, effect profile.");
        }
        [MenuItem("FlightSim/Framework/Create Navigation Mission")]
        public static void CreateMission()
        {
            string path=EditorUtility.SaveFilePanelInProject("Create Flight Mission","NewFlightMission","asset","Choose a project location");
            if(string.IsNullOrEmpty(path))return;
            var definition=TrainingMissionFactory.CreateNavigationTraining();
            definition.MissionId="CUSTOM_"+Guid.NewGuid().ToString("N").Substring(0,8).ToUpperInvariant();definition.DisplayName="新建航线任务";
            var asset=ScriptableObject.CreateInstance<MissionDefinitionAsset>();asset.SetDefinition(definition);AssetDatabase.CreateAsset(asset,path);Register(asset);Selection.activeObject=asset;
        }
        public static void Register(MissionDefinitionAsset mission)
        {
            Install();var library=AssetDatabase.LoadAssetAtPath<FlightMissionLibrary>(ResourceDirectory+"/MissionLibrary.asset");
            if(Array.IndexOf(library.Missions,mission)>=0)return;
            Undo.RecordObject(library,"Register flight mission");var missions=library.Missions;Array.Resize(ref missions,missions.Length+1);missions[missions.Length-1]=mission;library.Missions=missions;
            EditorUtility.SetDirty(library);AssetDatabase.SaveAssets();
        }
        [MenuItem("FlightSim/Framework/Open User Missions Folder")]
        public static void OpenMissionDirectory(){Directory.CreateDirectory(FlightMissionCatalog.UserMissionDirectory);EditorUtility.RevealInFinder(FlightMissionCatalog.UserMissionDirectory);}
        [MenuItem("FlightSim/Framework/Select Effect Profile")]
        public static void SelectEffects(){Install();Selection.activeObject=AssetDatabase.LoadAssetAtPath<FlightEffectProfile>(ResourceDirectory+"/DefaultEffects.asset");}
    }

    [CustomEditor(typeof(MissionDefinitionAsset))]
    public sealed class FlightMissionAssetInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("任务通过 MissionLibrary 注册到任务中心。飞行使用定义的独立副本，运行时不会改写本资源。",MessageType.Info);
            DrawDefaultInspector();
            var asset=(MissionDefinitionAsset)target;var validation=MissionDefinitionValidator.Validate(asset.Definition);
            EditorGUILayout.HelpBox(validation.IsValid?"任务定义校验通过。":validation.FirstError,validation.IsValid?MessageType.Info:MessageType.Error);
            using(new EditorGUI.DisabledScope(!validation.IsValid))
            {
                if(GUILayout.Button("注册到任务中心"))FlightFrameworkAuthoring.Register(asset);
                if(GUILayout.Button("导出 JSON"))
                {string path=EditorUtility.SaveFilePanel("Export Mission",Application.dataPath,asset.MissionId,"json");if(!string.IsNullOrEmpty(path))File.WriteAllText(path,asset.ExportJson());}
            }
            if(GUILayout.Button("从 JSON 导入"))
            {
                string path=EditorUtility.OpenFilePanel("Import Mission",Application.dataPath,"json");
                if(!string.IsNullOrEmpty(path))try{Undo.RecordObject(asset,"Import flight mission");asset.ImportJson(File.ReadAllText(path));EditorUtility.SetDirty(asset);}catch(Exception e){EditorUtility.DisplayDialog("任务无法导入",e.Message,"确定");}
            }
        }
    }
}

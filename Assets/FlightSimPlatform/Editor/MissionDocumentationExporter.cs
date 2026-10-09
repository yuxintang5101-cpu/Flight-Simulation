using System;
using System.IO;
using System.Text;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Missions;
using FlightSim.Platform.Unity;
using UnityEditor;
using UnityEngine;

namespace FlightSim.Platform.Editor
{
    public static class MissionDocumentationExporter
    {
        private const string OutputDirectory = "Assets/FlightSimPlatform/Docs/Integration/Missions";

        [MenuItem("FlightSim/Platform/Export Mission JSON Examples")]
        public static void ExportFromEditor()
        {
            ExportAll();
        }

        public static void ExportCli()
        {
            int exitCode = 0;
            try
            {
                ExportAll();
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                exitCode = 1;
            }
            finally
            {
                EditorApplication.Exit(exitCode);
            }
        }

        private static void ExportAll()
        {
            Directory.CreateDirectory(OutputDirectory);
            MissionDefinition[] missions = DefaultMissionCatalog.CreateAll();
            for (int index = 0; index < missions.Length; index++)
            {
                MissionDefinition mission = missions[index];
                string path = Path.Combine(OutputDirectory, mission.MissionId + ".json");
                File.WriteAllText(path, MissionJsonCodec.ToJson(mission, true), new UTF8Encoding(false));
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }
            AssetDatabase.SaveAssets();
            Debug.Log($"Exported {missions.Length} mission examples to {OutputDirectory}.");
        }
    }
}

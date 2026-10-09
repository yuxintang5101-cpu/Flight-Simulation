using System;

namespace FlightSim.Platform.Editor
{
    public static class PlatformAssetLayout
    {
        public const string PlatformRoot = "Assets/FlightSimPlatform";
        public const string F35Root = PlatformRoot + "/ThirdParty/F35";
        public const string F35MaterialRoot = F35Root + "/Materials";
        public const string EnvironmentMaterialRoot = PlatformRoot + "/Environment/Materials";
        public const string ScenePath = PlatformRoot + "/Samples/Scenes/FlightSim_KTEX_V2.unity";
        public const string CesiumServerPath = PlatformRoot + "/Config/FlightSimCesiumIonServer.asset";
        public const long TerrainIonAssetId = 1;
        public const long ImageryIonAssetId = 3830182;

        public static readonly string[] RuntimeExportRoots =
        {
            PlatformRoot + "/Config",
            PlatformRoot + "/Contracts",
            PlatformRoot + "/Core",
            PlatformRoot + "/Data",
            PlatformRoot + "/Docs",
            PlatformRoot + "/Environment",
            PlatformRoot + "/Integration",
            PlatformRoot + "/Materials",
            PlatformRoot + "/Missions",
            PlatformRoot + "/Prefabs",
            PlatformRoot + "/Presentation",
            PlatformRoot + "/Samples",
            PlatformRoot + "/Shaders",
            PlatformRoot + "/ThirdParty",
            PlatformRoot + "/Unity"
        };

        public static readonly string[] TestExportRoots =
        {
            PlatformRoot + "/Tests",
            PlatformRoot + "/Editor/Tests",
            PlatformRoot + "/Presentation/Tests",
            PlatformRoot + "/Unity/Tests"
        };

        public static readonly string[] ForbiddenRoots =
        {
            "Assets/External/F16C",
            "Assets/External/F35",
            "Assets/Scenes/FlightSim_KTEX_V2.unity",
            "Assets/CesiumSettings",
            "Packages/",
            "Library/",
            "Temp/",
            "Logs/",
            "Artifacts/"
        };

        public static bool IsAllowedRuntimeAsset(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            if (!path.StartsWith(PlatformRoot + "/", StringComparison.Ordinal) &&
                !string.Equals(path, PlatformRoot, StringComparison.Ordinal))
            {
                return false;
            }

            for (int i = 0; i < TestExportRoots.Length; i++)
            {
                if (IsAtOrBelow(path, TestExportRoots[i]))
                    return false;
            }

            return true;
        }

        private static bool IsAtOrBelow(string path, string root)
        {
            return string.Equals(path, root, StringComparison.Ordinal) ||
                   path.StartsWith(root + "/", StringComparison.Ordinal);
        }
    }
}

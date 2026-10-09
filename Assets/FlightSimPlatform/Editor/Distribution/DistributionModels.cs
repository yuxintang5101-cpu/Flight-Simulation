using System;

namespace FlightSim.Platform.Editor
{
    [Serializable]
    public sealed class AssetManifest
    {
        public string DistributionVersion = FlightSimDistributionBuilder.DistributionVersion;
        public string UnityVersion = FlightSimDistributionBuilder.RequiredUnityVersion;
        public string CesiumVersion = FlightSimDistributionBuilder.RequiredCesiumVersion;
        public AssetManifestEntry[] Assets = Array.Empty<AssetManifestEntry>();
    }

    [Serializable]
    public sealed class AssetManifestEntry
    {
        public string Path = string.Empty;
        public string Guid = string.Empty;
        public long SizeBytes;
        public string Sha256 = string.Empty;
    }

    [Serializable]
    public sealed class DistributionVersionInfo
    {
        public string DistributionVersion = FlightSimDistributionBuilder.DistributionVersion;
        public string UnityVersion = FlightSimDistributionBuilder.RequiredUnityVersion;
        public string RenderPipeline = "Built-in";
        public string TargetPlatform = "Windows x86-64";
        public string CesiumPackage = "com.cesium.unity";
        public string CesiumVersion = FlightSimDistributionBuilder.RequiredCesiumVersion;
        public int ContractVersion = 2;
        public int MissionSchemaVersion = 1;
        public string TokenSha256Prefix = string.Empty;
    }
}

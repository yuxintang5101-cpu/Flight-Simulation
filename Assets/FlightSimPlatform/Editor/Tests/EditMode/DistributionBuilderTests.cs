using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace FlightSim.Platform.Editor.Tests
{
    public sealed class DistributionBuilderTests
    {
        private string tempRoot;

        [SetUp]
        public void SetUp()
        {
            tempRoot = Path.Combine(Path.GetTempPath(), "FlightSimDistributionTests", Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempRoot);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(tempRoot))
                Directory.Delete(tempRoot, true);
        }

        [Test]
        public void ManifestEntriesAreSortedAndContainHashes()
        {
            string first = Path.Combine(tempRoot, "zeta.txt");
            string second = Path.Combine(tempRoot, "alpha.txt");
            File.WriteAllText(first, "zeta");
            File.WriteAllText(second, "alpha");

            AssetManifest manifest = AssetManifestBuilder.BuildFileManifest(
                tempRoot,
                new[] { first, second },
                _ => "guid");

            Assert.That(manifest.Assets.Select(entry => entry.Path), Is.Ordered);
            Assert.That(manifest.Assets.All(entry => entry.SizeBytes > 0), Is.True);
            Assert.That(manifest.Assets.All(entry => entry.Sha256.Length == 64), Is.True);
        }

        [Test]
        public void ChecksumFileCoversEveryDeliveryFileExceptItself()
        {
            File.WriteAllText(Path.Combine(tempRoot, "b.txt"), "b");
            Directory.CreateDirectory(Path.Combine(tempRoot, "sub"));
            File.WriteAllText(Path.Combine(tempRoot, "sub", "a.txt"), "a");

            string checksums = DistributionFileUtility.BuildChecksumText(tempRoot, "Checksums-SHA256.txt");

            Assert.That(checksums, Does.Contain("b.txt"));
            Assert.That(checksums, Does.Contain("sub/a.txt"));
            Assert.That(checksums, Does.Not.Contain("Checksums-SHA256.txt"));
        }

        [Test]
        public void TokenFingerprintNeverContainsTokenPlaintext()
        {
            const string token = "not-a-real-token-value";
            string fingerprint = DistributionFileUtility.ComputeTokenFingerprint(token);

            Assert.That(fingerprint, Has.Length.EqualTo(12));
            Assert.That(fingerprint, Does.Match("^[0-9a-f]{12}$"));
            Assert.That(fingerprint, Does.Not.Contain(token));
        }

        [Test]
        public void ForbiddenPathsAreDetectedBeforeExport()
        {
            string[] rejected = FlightSimDistributionBuilder.FindForbiddenPaths(new[]
            {
                "Assets/FlightSimPlatform/Core/FuelModel.cs",
                "Assets/External/F16C/model.fbx",
                "Packages/com.cesium.unity/package.json"
            });

            Assert.That(rejected, Is.EquivalentTo(new[]
            {
                "Assets/External/F16C/model.fbx",
                "Packages/com.cesium.unity/package.json"
            }));
        }
    }
}

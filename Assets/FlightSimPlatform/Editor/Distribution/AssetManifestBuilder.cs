using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FlightSim.Platform.Editor
{
    public static class AssetManifestBuilder
    {
        public static AssetManifest BuildFileManifest(
            string rootDirectory,
            IEnumerable<string> files,
            Func<string, string> guidProvider)
        {
            if (string.IsNullOrWhiteSpace(rootDirectory))
                throw new ArgumentException("A manifest root directory is required.", nameof(rootDirectory));
            if (files == null)
                throw new ArgumentNullException(nameof(files));
            if (guidProvider == null)
                throw new ArgumentNullException(nameof(guidProvider));

            string root = EnsureTrailingSeparator(Path.GetFullPath(rootDirectory));
            var entries = new List<AssetManifestEntry>();
            foreach (string file in files)
            {
                string fullPath = Path.GetFullPath(file);
                if (!fullPath.StartsWith(root, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException($"Manifest file is outside the root: {fullPath}");
                if (!File.Exists(fullPath))
                    throw new FileNotFoundException("Manifest input file does not exist.", fullPath);

                string relative = NormalizePath(fullPath.Substring(root.Length));
                entries.Add(new AssetManifestEntry
                {
                    Path = relative,
                    Guid = guidProvider(relative) ?? string.Empty,
                    SizeBytes = new FileInfo(fullPath).Length,
                    Sha256 = DistributionFileUtility.ComputeFileSha256(fullPath)
                });
            }

            return new AssetManifest
            {
                Assets = entries.OrderBy(entry => entry.Path, StringComparer.Ordinal).ToArray()
            };
        }

        private static string EnsureTrailingSeparator(string path)
        {
            return path.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal)
                ? path
                : path + Path.DirectorySeparatorChar;
        }

        private static string NormalizePath(string path)
        {
            return path.Replace('\\', '/');
        }
    }

    public static class DistributionFileUtility
    {
        public static string ComputeFileSha256(string path)
        {
            using (SHA256 sha = SHA256.Create())
            using (FileStream stream = File.OpenRead(path))
                return ToHex(sha.ComputeHash(stream));
        }

        public static string ComputeTokenFingerprint(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return string.Empty;
            using (SHA256 sha = SHA256.Create())
            {
                string hash = ToHex(sha.ComputeHash(Encoding.UTF8.GetBytes(token)));
                return hash.Substring(0, 12);
            }
        }

        public static string BuildChecksumText(string rootDirectory, string checksumFileName)
        {
            string root = Path.GetFullPath(rootDirectory);
            string[] files = Directory.GetFiles(root, "*", SearchOption.AllDirectories)
                .Where(path => !string.Equals(
                    Path.GetFileName(path),
                    checksumFileName,
                    StringComparison.OrdinalIgnoreCase))
                .OrderBy(path => NormalizeRelative(root, path), StringComparer.Ordinal)
                .ToArray();

            var builder = new StringBuilder();
            for (int i = 0; i < files.Length; i++)
            {
                builder.Append(ComputeFileSha256(files[i]));
                builder.Append("  ");
                builder.Append(NormalizeRelative(root, files[i]));
                builder.Append('\n');
            }
            return builder.ToString();
        }

        public static void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            if (!Directory.Exists(sourceDirectory))
                throw new DirectoryNotFoundException(sourceDirectory);
            Directory.CreateDirectory(targetDirectory);
            foreach (string directory in Directory.GetDirectories(sourceDirectory, "*", SearchOption.AllDirectories))
            {
                string relative = NormalizeRelative(sourceDirectory, directory);
                Directory.CreateDirectory(Path.Combine(targetDirectory, relative.Replace('/', Path.DirectorySeparatorChar)));
            }
            foreach (string file in Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories))
            {
                string relative = NormalizeRelative(sourceDirectory, file);
                string destination = Path.Combine(targetDirectory, relative.Replace('/', Path.DirectorySeparatorChar));
                Directory.CreateDirectory(Path.GetDirectoryName(destination));
                File.Copy(file, destination, false);
            }
        }

        private static string NormalizeRelative(string root, string path)
        {
            string normalizedRoot = Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
            string fullPath = Path.GetFullPath(path);
            if (!fullPath.StartsWith(normalizedRoot, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"Path is outside root: {fullPath}");
            return fullPath.Substring(normalizedRoot.Length).Replace('\\', '/');
        }

        private static string ToHex(byte[] bytes)
        {
            var builder = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                builder.Append(bytes[i].ToString("x2"));
            return builder.ToString();
        }
    }
}

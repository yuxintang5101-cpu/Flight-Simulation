using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace FlightSim.Bootstrap.Editor
{
    public sealed class BootstrapScopedRegistry
    {
        public string Name;
        public string Url;
        public List<string> Scopes = new List<string>();
    }

    public sealed class BootstrapManifestDocument
    {
        public List<BootstrapScopedRegistry> ScopedRegistries = new List<BootstrapScopedRegistry>();
        public Dictionary<string, string> Dependencies = new Dictionary<string, string>(StringComparer.Ordinal);
    }

    public static class BootstrapJson
    {
        public static string MergeManifest(string manifestJson)
        {
            JsonValue root = Parse(manifestJson);
            if (root.Kind != JsonKind.Object) throw new FormatException("Package manifest must be a JSON object.");

            JsonValue registries = JsonValue.Array();
            JsonValue currentRegistries;
            List<JsonValue> cesiumRegistries = new List<JsonValue>();
            List<JsonValue> otherRegistries = new List<JsonValue>();
            if (root.ObjectValue.TryGetValue("scopedRegistries", out currentRegistries) && currentRegistries.Kind == JsonKind.Array)
            {
                foreach (JsonValue registry in currentRegistries.ArrayValue)
                {
                    if (IsCesiumRegistry(registry)) cesiumRegistries.Add(registry);
                    else otherRegistries.Add(registry);
                }
            }

            JsonValue canonical = SelectCanonicalCesiumRegistry(cesiumRegistries) ?? CreateCesiumRegistry();
            MergeCesiumRegistries(canonical, cesiumRegistries);
            registries.ArrayValue.Add(canonical);
            registries.ArrayValue.AddRange(otherRegistries);

            JsonValue dependencies;
            if (!root.ObjectValue.TryGetValue("dependencies", out dependencies) || dependencies.Kind != JsonKind.Object)
            {
                dependencies = JsonValue.Object();
                root.ObjectValue["dependencies"] = dependencies;
            }

            dependencies.ObjectValue[BootstrapConstants.CesiumPackage] = JsonValue.String(BootstrapConstants.CesiumVersion);
            dependencies.ObjectValue[BootstrapConstants.TextMeshProPackage] = JsonValue.String(BootstrapConstants.TextMeshProVersion);
            dependencies.ObjectValue[BootstrapConstants.UguiPackage] = JsonValue.String(BootstrapConstants.UguiVersion);
            dependencies.ObjectValue[BootstrapConstants.InputSystemPackage] = JsonValue.String(BootstrapConstants.InputSystemVersion);
            root.ObjectValue["scopedRegistries"] = registries;
            return Write(root, true);
        }

        public static BootstrapManifestDocument ParseManifest(string manifestJson)
        {
            JsonValue root = Parse(manifestJson);
            if (root.Kind != JsonKind.Object) throw new FormatException("Package manifest must be a JSON object.");
            BootstrapManifestDocument result = new BootstrapManifestDocument();
            JsonValue registries;
            if (root.ObjectValue.TryGetValue("scopedRegistries", out registries) && registries.Kind == JsonKind.Array)
            {
                foreach (JsonValue item in registries.ArrayValue)
                {
                    if (item.Kind != JsonKind.Object) continue;
                    BootstrapScopedRegistry registry = new BootstrapScopedRegistry
                    {
                        Name = ReadString(item, "name"),
                        Url = ReadString(item, "url")
                    };
                    JsonValue scopes;
                    if (item.ObjectValue.TryGetValue("scopes", out scopes) && scopes.Kind == JsonKind.Array)
                    {
                        registry.Scopes.AddRange(scopes.ArrayValue.Where(value => value.Kind == JsonKind.String).Select(value => value.StringValue));
                    }
                    result.ScopedRegistries.Add(registry);
                }
            }

            JsonValue dependencies;
            if (root.ObjectValue.TryGetValue("dependencies", out dependencies) && dependencies.Kind == JsonKind.Object)
            {
                foreach (KeyValuePair<string, JsonValue> dependency in dependencies.ObjectValue)
                {
                    if (dependency.Value.Kind == JsonKind.String) result.Dependencies[dependency.Key] = dependency.Value.StringValue;
                }
            }
            return result;
        }

        public static BootstrapAssetIdentity[] ParseAssetManifest(string manifestJson)
        {
            JsonValue root = Parse(manifestJson);
            if (root.Kind != JsonKind.Object) throw new InvalidDataException("Asset manifest must be a JSON object.");
            JsonValue assets;
            if (!TryGetProperty(root, out assets, "Assets", "assets") || assets.Kind != JsonKind.Array)
                throw new InvalidDataException("Asset manifest is missing its Assets array.");

            Dictionary<string, BootstrapAssetIdentity> platformAssets = new Dictionary<string, BootstrapAssetIdentity>(StringComparer.Ordinal);
            foreach (JsonValue item in assets.ArrayValue)
            {
                if (item.Kind != JsonKind.Object) throw new InvalidDataException("Asset manifest entries must be JSON objects.");
                string path = ReadString(item, "Path");
                if (string.IsNullOrEmpty(path)) path = ReadString(item, "path");
                path = (path ?? string.Empty).Replace('\\', '/');
                if (!IsPlatformPath(path)) continue;
                string guid = ReadString(item, "Guid");
                if (string.IsNullOrEmpty(guid)) guid = ReadString(item, "guid");
                if (string.IsNullOrWhiteSpace(guid)) throw new InvalidDataException("Platform asset GUID is missing at " + path + ".");
                BootstrapAssetIdentity existing;
                if (platformAssets.TryGetValue(path, out existing))
                {
                    if (!string.Equals(existing.Guid, guid, StringComparison.Ordinal))
                        throw new InvalidDataException("Asset manifest contains conflicting GUIDs at " + path + ".");
                    continue;
                }
                platformAssets.Add(path, new BootstrapAssetIdentity { Path = path, Guid = guid });
            }
            if (platformAssets.Count == 0) throw new InvalidDataException("Asset manifest contains no FlightSim platform assets.");
            return platformAssets.Values.OrderBy(asset => asset.Path, StringComparer.Ordinal).ToArray();
        }

        public static string SerializeInstallState(BootstrapInstallState state)
        {
            JsonValue root = JsonValue.Object();
            root.ObjectValue["phase"] = JsonValue.String(state.Phase.ToString());
            root.ObjectValue["projectPath"] = JsonValue.String(state.ProjectPath ?? string.Empty);
            root.ObjectValue["manifestPath"] = JsonValue.String(state.ManifestPath ?? string.Empty);
            root.ObjectValue["assetManifestPath"] = JsonValue.String(state.AssetManifestPath ?? string.Empty);
            root.ObjectValue["platformPackagePath"] = JsonValue.String(state.PlatformPackagePath ?? string.Empty);
            root.ObjectValue["platformAlreadyImported"] = JsonValue.Boolean(state.PlatformAlreadyImported);
            root.ObjectValue["platformImportRequested"] = JsonValue.Boolean(state.PlatformImportRequested);
            root.ObjectValue["backupDirectory"] = JsonValue.String(state.Backup == null ? string.Empty : state.Backup.DirectoryPath ?? string.Empty);
            root.ObjectValue["failureMessage"] = JsonValue.String(state.FailureMessage ?? string.Empty);
            root.ObjectValue["updatedUtcTicks"] = JsonValue.Number(state.UpdatedUtcTicks.ToString(CultureInfo.InvariantCulture));
            JsonValue entries = JsonValue.Array();
            if (state.Backup != null)
            {
                foreach (BootstrapBackupEntry entry in state.Backup.Entries)
                {
                    JsonValue item = JsonValue.Object();
                    item.ObjectValue["sourcePath"] = JsonValue.String(entry.SourcePath ?? string.Empty);
                    item.ObjectValue["backupPath"] = JsonValue.String(entry.BackupPath ?? string.Empty);
                    item.ObjectValue["existed"] = JsonValue.Boolean(entry.Existed);
                    entries.ArrayValue.Add(item);
                }
            }
            root.ObjectValue["backupEntries"] = entries;
            JsonValue importedAssets = JsonValue.Array();
            foreach (BootstrapImportedAsset asset in state.ImportedAssets ?? new List<BootstrapImportedAsset>())
            {
                JsonValue item = JsonValue.Object();
                item.ObjectValue["path"] = JsonValue.String(asset.Path ?? string.Empty);
                item.ObjectValue["guid"] = JsonValue.String(asset.Guid ?? string.Empty);
                item.ObjectValue["existedBeforeImport"] = JsonValue.Boolean(asset.ExistedBeforeImport);
                item.ObjectValue["importedByInstaller"] = JsonValue.Boolean(asset.ImportedByInstaller);
                importedAssets.ArrayValue.Add(item);
            }
            root.ObjectValue["importedAssets"] = importedAssets;
            return Write(root, true);
        }

        public static BootstrapInstallState ParseInstallState(string json)
        {
            JsonValue root = Parse(json);
            if (root.Kind != JsonKind.Object) throw new FormatException("Install state must be a JSON object.");
            BootstrapInstallState state = new BootstrapInstallState
            {
                ProjectPath = ReadString(root, "projectPath"),
                ManifestPath = ReadString(root, "manifestPath"),
                AssetManifestPath = ReadString(root, "assetManifestPath"),
                PlatformPackagePath = ReadString(root, "platformPackagePath"),
                PlatformAlreadyImported = ReadBoolean(root, "platformAlreadyImported"),
                PlatformImportRequested = ReadBoolean(root, "platformImportRequested"),
                FailureMessage = ReadString(root, "failureMessage"),
                UpdatedUtcTicks = ReadLong(root, "updatedUtcTicks")
            };
            BootstrapInstallPhase phase;
            if (!Enum.TryParse(ReadString(root, "phase"), out phase)) throw new FormatException("Install state has an unknown phase.");
            state.Phase = phase;
            state.Backup = new BootstrapBackup { DirectoryPath = ReadString(root, "backupDirectory") };
            JsonValue entries;
            if (root.ObjectValue.TryGetValue("backupEntries", out entries) && entries.Kind == JsonKind.Array)
            {
                foreach (JsonValue item in entries.ArrayValue.Where(value => value.Kind == JsonKind.Object))
                {
                    state.Backup.Entries.Add(new BootstrapBackupEntry
                    {
                        SourcePath = ReadString(item, "sourcePath"),
                        BackupPath = ReadString(item, "backupPath"),
                        Existed = ReadBoolean(item, "existed")
                    });
                }
            }
            JsonValue importedAssets;
            if (root.ObjectValue.TryGetValue("importedAssets", out importedAssets) && importedAssets.Kind == JsonKind.Array)
            {
                foreach (JsonValue item in importedAssets.ArrayValue.Where(value => value.Kind == JsonKind.Object))
                {
                    state.ImportedAssets.Add(new BootstrapImportedAsset
                    {
                        Path = ReadString(item, "path"),
                        Guid = ReadString(item, "guid"),
                        ExistedBeforeImport = ReadBoolean(item, "existedBeforeImport"),
                        ImportedByInstaller = ReadBoolean(item, "importedByInstaller")
                    });
                }
            }
            return state;
        }

        public static string EscapeString(string value)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char character in value ?? string.Empty)
            {
                switch (character)
                {
                    case '\\': builder.Append("\\\\"); break;
                    case '"': builder.Append("\\\""); break;
                    case '\b': builder.Append("\\b"); break;
                    case '\f': builder.Append("\\f"); break;
                    case '\n': builder.Append("\\n"); break;
                    case '\r': builder.Append("\\r"); break;
                    case '\t': builder.Append("\\t"); break;
                    default:
                        if (character < ' ') builder.Append("\\u").Append(((int)character).ToString("x4"));
                        else builder.Append(character);
                        break;
                }
            }
            return builder.ToString();
        }

        private static JsonValue CreateCesiumRegistry()
        {
            JsonValue registry = JsonValue.Object();
            registry.ObjectValue["name"] = JsonValue.String("Cesium");
            registry.ObjectValue["url"] = JsonValue.String(BootstrapConstants.CesiumRegistryUrl);
            JsonValue scopes = JsonValue.Array();
            scopes.ArrayValue.Add(JsonValue.String(BootstrapConstants.CesiumScope));
            registry.ObjectValue["scopes"] = scopes;
            return registry;
        }

        private static JsonValue SelectCanonicalCesiumRegistry(List<JsonValue> registries)
        {
            return registries.FirstOrDefault(registry => string.Equals(ReadString(registry, "url"), BootstrapConstants.CesiumRegistryUrl, StringComparison.Ordinal)) ??
                   registries.FirstOrDefault();
        }

        private static void MergeCesiumRegistries(JsonValue canonical, List<JsonValue> registries)
        {
            canonical.ObjectValue["name"] = JsonValue.String("Cesium");
            canonical.ObjectValue["url"] = JsonValue.String(BootstrapConstants.CesiumRegistryUrl);
            List<string> scopes = new List<string>();
            foreach (JsonValue registry in registries)
            {
                foreach (KeyValuePair<string, JsonValue> property in registry.ObjectValue)
                {
                    if (property.Key == "name" || property.Key == "url" || property.Key == "scopes") continue;
                    if (!canonical.ObjectValue.ContainsKey(property.Key)) canonical.ObjectValue[property.Key] = property.Value;
                }
                JsonValue registryScopes;
                if (!registry.ObjectValue.TryGetValue("scopes", out registryScopes) || registryScopes.Kind != JsonKind.Array) continue;
                foreach (JsonValue scope in registryScopes.ArrayValue)
                {
                    if (scope.Kind == JsonKind.String && !scopes.Contains(scope.StringValue)) scopes.Add(scope.StringValue);
                }
            }
            if (!scopes.Contains(BootstrapConstants.CesiumScope)) scopes.Add(BootstrapConstants.CesiumScope);
            JsonValue mergedScopes = JsonValue.Array();
            foreach (string scope in scopes) mergedScopes.ArrayValue.Add(JsonValue.String(scope));
            canonical.ObjectValue["scopes"] = mergedScopes;
        }

        private static bool IsCesiumRegistry(JsonValue registry)
        {
            if (registry.Kind != JsonKind.Object) return false;
            if (string.Equals(ReadString(registry, "name"), "Cesium", StringComparison.Ordinal)) return true;
            JsonValue scopes;
            return registry.ObjectValue.TryGetValue("scopes", out scopes) && scopes.Kind == JsonKind.Array &&
                   scopes.ArrayValue.Any(value => value.Kind == JsonKind.String && string.Equals(value.StringValue, BootstrapConstants.CesiumScope, StringComparison.Ordinal));
        }

        private static bool IsPlatformPath(string path)
        {
            bool isPlatform = string.Equals(path, BootstrapConstants.PlatformRoot, StringComparison.Ordinal) ||
                              path.StartsWith(BootstrapConstants.PlatformRoot + "/", StringComparison.Ordinal);
            return isPlatform && path.IndexOf("/Tests/", StringComparison.OrdinalIgnoreCase) < 0;
        }

        private static bool TryGetProperty(JsonValue value, out JsonValue result, params string[] names)
        {
            foreach (string name in names)
            {
                if (value.ObjectValue.TryGetValue(name, out result)) return true;
            }
            result = null;
            return false;
        }

        private static string ReadString(JsonValue value, string key)
        {
            JsonValue item;
            return value.ObjectValue.TryGetValue(key, out item) && item.Kind == JsonKind.String ? item.StringValue : string.Empty;
        }

        private static long ReadLong(JsonValue value, string key)
        {
            JsonValue item;
            long parsed;
            return value.ObjectValue.TryGetValue(key, out item) && item.Kind == JsonKind.Number && long.TryParse(item.StringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed) ? parsed : 0L;
        }

        private static bool ReadBoolean(JsonValue value, string key)
        {
            JsonValue item;
            return value.ObjectValue.TryGetValue(key, out item) && item.Kind == JsonKind.Boolean && item.BooleanValue;
        }

        private static JsonValue Parse(string json)
        {
            if (json == null) throw new ArgumentNullException(nameof(json));
            return new JsonParser(json).Parse();
        }

        private static string Write(JsonValue value, bool pretty)
        {
            StringBuilder builder = new StringBuilder();
            WriteValue(builder, value, pretty, 0);
            return builder.ToString();
        }

        private static void WriteValue(StringBuilder builder, JsonValue value, bool pretty, int depth)
        {
            switch (value.Kind)
            {
                case JsonKind.Object:
                    builder.Append('{');
                    WriteItems(builder, value.ObjectValue.OrderBy(pair => pair.Key, StringComparer.Ordinal).Select(pair => new KeyValuePair<string, JsonValue>(pair.Key, pair.Value)), pretty, depth, true);
                    builder.Append('}');
                    break;
                case JsonKind.Array:
                    builder.Append('[');
                    WriteItems(builder, value.ArrayValue.Select((item, index) => new KeyValuePair<string, JsonValue>(index.ToString(CultureInfo.InvariantCulture), item)), pretty, depth, false);
                    builder.Append(']');
                    break;
                case JsonKind.String: builder.Append('"').Append(EscapeString(value.StringValue)).Append('"'); break;
                case JsonKind.Number: builder.Append(value.StringValue); break;
                case JsonKind.Boolean: builder.Append(value.BooleanValue ? "true" : "false"); break;
                default: builder.Append("null"); break;
            }
        }

        private static void WriteItems(StringBuilder builder, IEnumerable<KeyValuePair<string, JsonValue>> values, bool pretty, int depth, bool includeKeys)
        {
            KeyValuePair<string, JsonValue>[] items = values.ToArray();
            for (int index = 0; index < items.Length; index++)
            {
                if (pretty) builder.Append('\n').Append(' ', (depth + 1) * 2);
                if (includeKeys) builder.Append('"').Append(EscapeString(items[index].Key)).Append(pretty ? "\": " : "\":");
                WriteValue(builder, items[index].Value, pretty, depth + 1);
                if (index < items.Length - 1) builder.Append(',');
            }
            if (items.Length > 0 && pretty) builder.Append('\n').Append(' ', depth * 2);
        }

        private enum JsonKind { Object, Array, String, Number, Boolean, Null }

        private sealed class JsonValue
        {
            public JsonKind Kind;
            public Dictionary<string, JsonValue> ObjectValue;
            public List<JsonValue> ArrayValue;
            public string StringValue;
            public bool BooleanValue;

            public static JsonValue Object() { return new JsonValue { Kind = JsonKind.Object, ObjectValue = new Dictionary<string, JsonValue>(StringComparer.Ordinal) }; }
            public static JsonValue Array() { return new JsonValue { Kind = JsonKind.Array, ArrayValue = new List<JsonValue>() }; }
            public static JsonValue String(string value) { return new JsonValue { Kind = JsonKind.String, StringValue = value ?? string.Empty }; }
            public static JsonValue Number(string value) { return new JsonValue { Kind = JsonKind.Number, StringValue = value }; }
            public static JsonValue Boolean(bool value) { return new JsonValue { Kind = JsonKind.Boolean, BooleanValue = value }; }
            public static JsonValue Null() { return new JsonValue { Kind = JsonKind.Null }; }
        }

        private sealed class JsonParser
        {
            private readonly string _json;
            private int _position;
            public JsonParser(string json) { _json = json; }

            public JsonValue Parse()
            {
                JsonValue value = ParseValue();
                SkipWhitespace();
                if (_position != _json.Length) throw Error("Unexpected trailing JSON content.");
                return value;
            }

            private JsonValue ParseValue()
            {
                SkipWhitespace();
                if (_position >= _json.Length) throw Error("Unexpected end of JSON.");
                switch (_json[_position])
                {
                    case '{': return ParseObject();
                    case '[': return ParseArray();
                    case '"': return JsonValue.String(ParseString());
                    case 't': Expect("true"); return JsonValue.Boolean(true);
                    case 'f': Expect("false"); return JsonValue.Boolean(false);
                    case 'n': Expect("null"); return JsonValue.Null();
                    default: return JsonValue.Number(ParseNumber());
                }
            }

            private JsonValue ParseObject()
            {
                _position++;
                JsonValue value = JsonValue.Object();
                SkipWhitespace();
                if (TryConsume('}')) return value;
                while (true)
                {
                    SkipWhitespace();
                    if (!TryConsume('"')) throw Error("Object property name must be a string.");
                    _position--;
                    string key = ParseString();
                    SkipWhitespace();
                    Require(':');
                    value.ObjectValue[key] = ParseValue();
                    SkipWhitespace();
                    if (TryConsume('}')) return value;
                    Require(',');
                }
            }

            private JsonValue ParseArray()
            {
                _position++;
                JsonValue value = JsonValue.Array();
                SkipWhitespace();
                if (TryConsume(']')) return value;
                while (true)
                {
                    value.ArrayValue.Add(ParseValue());
                    SkipWhitespace();
                    if (TryConsume(']')) return value;
                    Require(',');
                }
            }

            private string ParseString()
            {
                Require('"');
                StringBuilder builder = new StringBuilder();
                while (_position < _json.Length)
                {
                    char character = _json[_position++];
                    if (character == '"') return builder.ToString();
                    if (character != '\\') { builder.Append(character); continue; }
                    if (_position >= _json.Length) throw Error("Unterminated JSON escape sequence.");
                    char escape = _json[_position++];
                    switch (escape)
                    {
                        case '"': builder.Append('"'); break;
                        case '\\': builder.Append('\\'); break;
                        case '/': builder.Append('/'); break;
                        case 'b': builder.Append('\b'); break;
                        case 'f': builder.Append('\f'); break;
                        case 'n': builder.Append('\n'); break;
                        case 'r': builder.Append('\r'); break;
                        case 't': builder.Append('\t'); break;
                        case 'u':
                            if (_position + 4 > _json.Length) throw Error("Invalid Unicode escape.");
                            int code;
                            if (!int.TryParse(_json.Substring(_position, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out code)) throw Error("Invalid Unicode escape.");
                            builder.Append((char)code);
                            _position += 4;
                            break;
                        default: throw Error("Invalid JSON escape sequence.");
                    }
                }
                throw Error("Unterminated JSON string.");
            }

            private string ParseNumber()
            {
                int start = _position;
                if (TryConsume('-')) { }
                ConsumeDigits();
                if (TryConsume('.')) ConsumeDigits();
                if (_position < _json.Length && (_json[_position] == 'e' || _json[_position] == 'E'))
                {
                    _position++;
                    if (_position < _json.Length && (_json[_position] == '+' || _json[_position] == '-')) _position++;
                    ConsumeDigits();
                }
                string number = _json.Substring(start, _position - start);
                double parsed;
                if (number.Length == 0 || !double.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out parsed)) throw Error("Invalid JSON number.");
                return number;
            }

            private void ConsumeDigits()
            {
                int start = _position;
                while (_position < _json.Length && char.IsDigit(_json[_position])) _position++;
                if (start == _position) throw Error("Expected JSON digits.");
            }

            private void Expect(string value)
            {
                if (_position + value.Length > _json.Length || string.CompareOrdinal(_json, _position, value, 0, value.Length) != 0) throw Error("Invalid JSON literal.");
                _position += value.Length;
            }

            private void Require(char expected)
            {
                SkipWhitespace();
                if (!TryConsume(expected)) throw Error("Expected '" + expected + "'.");
            }

            private bool TryConsume(char expected)
            {
                if (_position < _json.Length && _json[_position] == expected) { _position++; return true; }
                return false;
            }

            private void SkipWhitespace()
            {
                while (_position < _json.Length && char.IsWhiteSpace(_json[_position])) _position++;
            }

            private FormatException Error(string message) { return new FormatException(message + " Position: " + _position.ToString(CultureInfo.InvariantCulture)); }
        }
    }
}

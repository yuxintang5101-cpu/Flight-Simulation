using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using FlightSim.Platform.Contracts;

namespace FlightSim.Platform.Integration
{
    public static class NdjsonTelemetrySerializer
    {
        public static string SerializeHello(ushort contractVersion, ushort schemaVersion)
        {
            return "{\"type\":\"hello\",\"contractVersion\":" + contractVersion.ToString(CultureInfo.InvariantCulture) +
                   ",\"schemaVersion\":" + schemaVersion.ToString(CultureInfo.InvariantCulture) +
                   ",\"readOnly\":true,\"domains\":[\"identity\",\"fastState\",\"systemsState\",\"tacticalPicture\",\"combatState\",\"missionState\",\"missionActor\",\"missionObjective\",\"automationState\",\"simulationEvent\",\"missionEvent\",\"weaponEngagementEvent\",\"health\"]," +
                   "\"frequenciesHz\":{\"fastState\":50,\"systemsState\":10,\"combatState\":10,\"tacticalPicture\":5,\"missionState\":5,\"health\":5}}";
        }

        public static string SerializeEnvelope<T>(
            string type,
            string domain,
            ushort contractVersion,
            ushort schemaVersion,
            ulong tick,
            double simulationTimeS,
            AircraftId aircraftId,
            bool valid,
            T payload)
        {
            if (!string.Equals(type, "data", StringComparison.Ordinal) &&
                !string.Equals(type, "event", StringComparison.Ordinal))
                throw new ArgumentException("NDJSON envelope type must be data or event.", nameof(type));
            if (string.IsNullOrWhiteSpace(domain))
                throw new ArgumentException("NDJSON envelope domain is required.", nameof(domain));
            var builder = new StringBuilder(1024);
            builder.Append("{\"type\":");
            AppendString(builder, type);
            builder.Append(",\"domain\":");
            AppendString(builder, domain);
            builder.Append(",\"contractVersion\":").Append(contractVersion.ToString(CultureInfo.InvariantCulture));
            builder.Append(",\"schemaVersion\":").Append(schemaVersion.ToString(CultureInfo.InvariantCulture));
            builder.Append(",\"tick\":").Append(tick.ToString(CultureInfo.InvariantCulture));
            builder.Append(",\"simulationTimeS\":").Append(ToFiniteNumber(simulationTimeS));
            builder.Append(",\"aircraftId\":");
            AppendString(builder, aircraftId.Value);
            builder.Append(",\"valid\":").Append(valid ? "true" : "false");
            builder.Append(",\"payload\":");
            AppendValue(builder, payload, 0);
            builder.Append('}');
            return builder.ToString();
        }

        private static void AppendValue(StringBuilder builder, object value, int depth)
        {
            if (value == null) { builder.Append("null"); return; }
            if (depth > 16) throw new InvalidOperationException("NDJSON payload nesting exceeds 16 levels.");
            Type type = value.GetType();
            if (type == typeof(string)) { AppendString(builder, (string)value); return; }
            if (type == typeof(char)) { AppendString(builder, value.ToString()); return; }
            if (type == typeof(bool)) { builder.Append((bool)value ? "true" : "false"); return; }
            if (type == typeof(AircraftId)) { AppendString(builder, ((AircraftId)value).Value); return; }
            if (type.IsEnum) { AppendString(builder, value.ToString()); return; }
            if (IsNumber(type)) { AppendNumber(builder, value); return; }
            if (value is IEnumerable enumerable && !(value is string))
            {
                builder.Append('[');
                bool first = true;
                foreach (object item in enumerable)
                {
                    if (!first) builder.Append(',');
                    first = false;
                    AppendValue(builder, item, depth + 1);
                }
                builder.Append(']');
                return;
            }

            builder.Append('{');
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public)
                .OrderBy(field => field.MetadataToken)
                .ToArray();
            for (int i = 0; i < fields.Length; i++)
            {
                if (i > 0) builder.Append(',');
                AppendString(builder, fields[i].Name);
                builder.Append(':');
                AppendValue(builder, fields[i].GetValue(value), depth + 1);
            }
            builder.Append('}');
        }

        private static bool IsNumber(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte: case TypeCode.SByte: case TypeCode.Int16: case TypeCode.UInt16:
                case TypeCode.Int32: case TypeCode.UInt32: case TypeCode.Int64: case TypeCode.UInt64:
                case TypeCode.Single: case TypeCode.Double: case TypeCode.Decimal: return true;
                default: return false;
            }
        }

        private static void AppendNumber(StringBuilder builder, object value)
        {
            if (value is double doubleValue) { builder.Append(ToFiniteNumber(doubleValue)); return; }
            if (value is float floatValue) { builder.Append(ToFiniteNumber(floatValue)); return; }
            builder.Append(Convert.ToString(value, CultureInfo.InvariantCulture));
        }

        private static string ToFiniteNumber(double value)
        {
            return double.IsNaN(value) || double.IsInfinity(value) ? "null" : value.ToString("R", CultureInfo.InvariantCulture);
        }

        private static void AppendString(StringBuilder builder, string value)
        {
            builder.Append('"');
            string text = value ?? string.Empty;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                switch (c)
                {
                    case '"': builder.Append("\\\""); break;
                    case '\\': builder.Append("\\\\"); break;
                    case '\b': builder.Append("\\b"); break;
                    case '\f': builder.Append("\\f"); break;
                    case '\n': builder.Append("\\n"); break;
                    case '\r': builder.Append("\\r"); break;
                    case '\t': builder.Append("\\t"); break;
                    default:
                        if (c < 0x20) builder.Append("\\u").Append(((int)c).ToString("x4"));
                        else builder.Append(c);
                        break;
                }
            }
            builder.Append('"');
        }
    }
}

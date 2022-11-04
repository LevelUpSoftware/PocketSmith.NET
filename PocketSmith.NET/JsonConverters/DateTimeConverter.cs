using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketSmith.NET.JsonConverters;

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string stringValue = reader.GetString();
        if (stringValue != null)
        {
            var parseSuccess = DateTime.TryParse(stringValue, out DateTime dateResult);
            if (parseSuccess)
            {
                return dateResult;
            }
        }

        throw new JsonException($"Failed to parse Date. Input value: {stringValue}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("O"));
    }
}
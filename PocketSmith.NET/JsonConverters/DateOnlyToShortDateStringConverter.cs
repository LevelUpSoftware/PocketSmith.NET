using System.Text.Json;
using System.Text.Json.Serialization;
using PocketSmith.NET.Extensions;

namespace PocketSmith.NET.JsonConverters;

public class DateOnlyToShortDateStringConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string stringValue = reader.GetString();
        if (stringValue != null)
        {
            var parseSuccess = DateOnly.TryParse(stringValue, out DateOnly dateResult);
            if (parseSuccess)
            {
                return dateResult;
            }
        }

        throw new JsonException($"Failed to parse Date. Input value: {stringValue}");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToFormattedString());
    }
}
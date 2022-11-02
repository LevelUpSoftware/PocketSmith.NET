using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocketSmith.NET.JsonConverters;

public class AccountTypeJsonConverter : JsonConverter<PocketSmithAccountType>
{
    public override PocketSmithAccountType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? stringValue = null;
        if (reader.TokenType == JsonTokenType.String)
        {
            stringValue = reader.GetString();
            var parseSuccess = EnumExtensions.TryParse(stringValue!, out PocketSmithAccountType? accountType);
            if (parseSuccess)
            {
                return accountType.Value;
            }
        }

        throw new JsonException($"Failed to parse PocketSmithAccountType. Input value: {stringValue}");
    }

    public override void Write(Utf8JsonWriter writer, PocketSmithAccountType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetDisplayName());
    }
}
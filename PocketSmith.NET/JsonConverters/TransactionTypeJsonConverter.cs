﻿using System.Text.Json;
using System.Text.Json.Serialization;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using System.Globalization;

namespace PocketSmith.NET.JsonConverters;

public class TransactionTypeJsonConverter : JsonConverter<PocketSmithTransactionType>
{
    public override PocketSmithTransactionType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? stringValue = null;
        if (reader.TokenType == JsonTokenType.String)
        {
            stringValue = reader.GetString();
            var parseSuccess = EnumExtensions.TryParse(stringValue!, out PocketSmithTransactionType? transactionType);
            if (parseSuccess)
            {
                return transactionType.Value;
            }
        }

        throw new JsonException($"Failed to parse PocketSmithTransactionType. Input value: {stringValue}");
    }

    public override void Write(Utf8JsonWriter writer, PocketSmithTransactionType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetDisplayName());
    }
}
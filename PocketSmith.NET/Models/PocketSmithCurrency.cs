using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;

namespace PocketSmith.NET.Models;

[HttpRoute("currencies")]
public class PocketSmithCurrency
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("minor_unit")]
    public int MinorUnit { get; set; }

    [JsonPropertyName("separators")]
    public PocketSmithCurrencySeparator? Separators
    {
        get;
        set;
    }
}
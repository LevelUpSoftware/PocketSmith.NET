using System.Text.Json.Serialization;

namespace PocketSmith.NET.Models;

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
using System.Text.Json.Serialization;

namespace PocketSmith.NET.Models;

public class PocketSmithCurrencySeparator
{
    [JsonPropertyName("major")]
    public string Major { get; set; }

    [JsonPropertyName("minor")]
    public string Minor { get; set; }
}
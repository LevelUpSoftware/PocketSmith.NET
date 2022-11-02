using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;

namespace PocketSmith.NET.Models;

[HttpRoute("time_zones")]
public class PocketSmithTimeZone
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("utc_offset")]
    public long UtcOffset { get; set; }

    [JsonPropertyName("formatted_name")]
    public string FormattedName { get; set; }

    [JsonPropertyName("formatted_offset")]
    public string FormattedOffset { get; set; }

    [JsonPropertyName("abbreviation")]
    public string Abbreviation { get; set; }

    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }
}
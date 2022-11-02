using System.Text.Json.Serialization;

namespace PocketSmith.NET.Models;

public class PocketSmithAttachmentVariants
{
    [JsonPropertyName("thumb_url")]
    public string ThumbUrl { get; set; }

    [JsonPropertyName("large_url")]
    public string LargeUrl { get; set; }
}
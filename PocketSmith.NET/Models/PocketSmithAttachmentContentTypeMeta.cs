using System.Text.Json.Serialization;

namespace PocketSmith.NET.Models;

public class PocketSmithAttachmentContentTypeMeta
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("extension")]
    public string Extension { get; set; }
}
using System.Text.Json;
using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;

namespace PocketSmith.NET.Models;

[HttpRoute("attachments")]
public class PocketSmithAttachment
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("file_name")]
    public string FileName { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("content_type")]
    public string ContentType { get; set; }

    [JsonPropertyName("content_type_meta")]
    public PocketSmithAttachmentContentTypeMeta ContentTypeMeta { get; set; }

    [JsonPropertyName("original_url")]
    public string OriginalUrl { get; set; }

    [JsonPropertyName("variants")]
    public PocketSmithAttachmentVariants Variants { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
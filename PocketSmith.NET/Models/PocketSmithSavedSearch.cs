using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;

namespace PocketSmith.NET.Models;

[HttpRoute("saved_searches")]
public class PocketSmithSavedSearch
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

}
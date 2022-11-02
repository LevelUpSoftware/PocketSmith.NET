using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;

namespace PocketSmith.NET.Models;

[HttpRoute("category_rules")]
public class PocketSmithCategoryRule
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("category")]
    public PocketSmithCategory Category { get; set; }

    [JsonPropertyName("payee_matches")]
    public string PayeeMatches { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;

namespace PocketSmith.NET.Models;
[HttpRoute("institutions")]
public class PocketSmithInstitution
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
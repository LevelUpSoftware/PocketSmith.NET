using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;

namespace PocketSmith.NET.Models;

[HttpRoute("categories")]
public class PocketSmithCategory
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("colour")]
    public string Colour { get; set; }

    [JsonPropertyName("is_transfer")]
    public bool IsTransfer { get; set; }

    [JsonPropertyName("is_bill")]
    public bool IsBill { get; set; }

    [JsonPropertyName("refund_behavior")]
    public string RefundBehavior { get; set; }

    [JsonPropertyName("children")]
    public List<PocketSmithCategory> Children { get; set; }

    [JsonPropertyName("parent_id")]
    public long? ParentId { get; set; }

    [JsonPropertyName("roll_up")]
    public bool RollUp { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

}
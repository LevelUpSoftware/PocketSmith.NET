using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;

namespace PocketSmith.NET.Models;

[HttpRoute("budget")]
public class PocketSmithBudget
{
    [JsonPropertyName("category")]
    public PocketSmithCategory Category { get; set; }

    [JsonPropertyName("is_transfer")]
    public bool IsTransfer { get; set; }

    [JsonPropertyName("expense")]
    public PocketSmithBudgetEvent? Expense { get; set; }

    [JsonPropertyName("income")]
    public PocketSmithBudgetEvent? Income { get; set; }

}
using System.Text.Json.Serialization;

namespace PocketSmith.NET.Services.TransactionAccounts.Models;

public class UpdatePocketSmithTransactionAccount
{
    [JsonPropertyName("institution_id")]
    public int? InstitutionId { get; set; }

    [JsonPropertyName("starting_balance")]
    public double? StartingBalance { get; set; }

    [JsonPropertyName("starting_balance_date")]
    public DateOnly? StartingBalanceDate { get; set; }
}
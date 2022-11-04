using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;
using PocketSmith.NET.JsonConverters;

namespace PocketSmith.NET.Models;

[HttpRoute("accounts")]
public class PocketSmithAccount
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; }

    [JsonPropertyName("current_balance")]
    public double CurrentBalance { get; set; }

    [JsonPropertyName("current_balance_in_base_currency")]
    public double CurrentBalanceInBaseCurrency { get; set; }

    [JsonPropertyName("current_balance_exchange_rate")]
    public double? CurrentBalanceExchangeRate { get; set; }

    [JsonConverter(typeof(DateOnlyToShortDateStringConverter))]
    [JsonPropertyName("current_balance_date")]
    public DateOnly CurrentBalanceDate { get; set; }

    [JsonPropertyName("safe_balance")]
    public double? SafeBalance { get; set; }

    [JsonPropertyName("safe_balance_in_base_currency")]
    public double? SafeBalanceInBaseCurrency { get; set; }

    [JsonPropertyName("has_safe_balance_adjustment")]
    public bool HasSafeBalanceAdjustment { get; set; }

    [JsonConverter(typeof(AccountTypeJsonConverter))]
    [JsonPropertyName("type")]
    public PocketSmithAccountType Type { get; set; }

    [JsonPropertyName("is_net_worth")]
    public bool IsNetWorth { get; set; }

    [JsonPropertyName("primary_transaction_account")]
    public PocketSmithTransactionAccount PrimaryTransactionAccount { get; set; }

    [JsonPropertyName("primary_scenario")]
    public PocketSmithAccountScenario PrimaryScenario { get; set; }

    [JsonPropertyName("transaction_accounts")]
    public List<PocketSmithTransactionAccount> TransactionAccounts { get; set; }

    [JsonPropertyName("scenarios")]
    public List<PocketSmithAccountScenario> Scenarios { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
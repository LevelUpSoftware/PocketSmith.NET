using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;
using PocketSmith.NET.JsonConverters;

namespace PocketSmith.NET.Models;

[HttpRoute("transaction_accounts")]
public class PocketSmithTransactionAccount
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("account_id")]
    public long AccountId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("latest_feed_name")]
    public string LatestFeedName { get; set; }

    [JsonPropertyName("number")]
    public string Number { get; set; }

    [JsonConverter(typeof(AccountTypeJsonConverter))]
    [JsonPropertyName("type")]
    public PocketSmithAccountType Type { get; set; }

    [JsonPropertyName("offline")]
    public bool Offline { get; set; }

    [JsonPropertyName("is_net_worth")]
    public bool IsNetWorth { get; set; }

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

    [JsonPropertyName("current_balance_source")]
    public string CurrentBalanceSource { get; set; }

    [JsonPropertyName("data_feeds_balance_type")]
    public string DataFeedsBalanceType { get; set; }

    [JsonPropertyName("safe_balance")]
    public double? SafeBalance { get; set; }

    [JsonPropertyName("safe_balance_in_base_currency")]
    public double? SafeBalanceInBaseCurrency { get; set; }

    [JsonPropertyName("has_safe_balance_adjustment")]
    public bool? HasSafeBalanceAdjustment { get; set; }

    [JsonPropertyName("starting_balance")]
    public double StartingBalance { get; set; }

    [JsonConverter(typeof(DateOnlyToShortDateStringConverter))]
    [JsonPropertyName("starting_balance_date")]
    public DateOnly StartingBalanceDate { get; set; }

    [JsonPropertyName("institution")] 
    public PocketSmithInstitution Institution { get; set; }

    [JsonPropertyName("data_feeds_account_id")]
    public string DataFeedsAccountId { get; set; }

    [JsonPropertyName("data_feeds_connection_id")]
    public string DataFeedsConnectionId { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
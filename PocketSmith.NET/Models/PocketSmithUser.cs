using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;

namespace PocketSmith.NET.Models;

[HttpRoute("users")]
public class PocketSmithUser
{

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("login")]
    public string Login { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; }

    [JsonPropertyName("beta_user")]
    public bool BetaUser { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }

    [JsonPropertyName("time_zone")]
    public string TimeZone { get; set; }

    [JsonPropertyName("week_start_day")]
    public int WeekStartDay { get; set; }

    [JsonPropertyName("is_reviewing_transactions")]
    public bool IsReviewingTransactions { get; set; }

    [JsonPropertyName("base_currency_code")]
    public string BaseCurrencyCode { get; set; }

    [JsonPropertyName("always_show_base_currency")]
    public bool AlwaysShowBaseCurrency { get; set; }

    [JsonPropertyName("using_multiple_currencies")]
    public bool UsingMultipleCurrencies { get; set; }

    [JsonPropertyName("available_accounts")]
    public int AvailableAccounts { get; set; }

    [JsonPropertyName("available_budgets")]
    public int AvailableBudgets { get; set; }

    [JsonPropertyName("allowed_data_feeds")]
    public bool AllowedDataFeeds { get; set; }

    [JsonPropertyName("tell_a_friend_access")]
    public bool? TellAFriendAccess { get; set; }

    [JsonPropertyName("tell_a_friend_code")]
    public string TellAFriendCode { get; set; }

    [JsonPropertyName("forecast_last_updated_at")]
    public DateTime ForecastLastUpdatedAt { get; set; }
    
    [JsonPropertyName("forecast_last_accessed_at")]
    public DateTime ForecastLastAccessedAt { get; set; }

    [JsonPropertyName("forecast_start_date")]
    public string ForecastStartDate { get; set; }

    [JsonPropertyName("forecast_end_date")]
    public string ForecastEndDate { get; set; }

    [JsonPropertyName("forecast_defer_recalculate")]
    public bool ForecastDeferRecalculate { get; set; }

    [JsonPropertyName("forecast_needs_recalculate")]
    public bool ForecastNeedsRecalculate { get; set; }

    [JsonPropertyName("last_logged_in_at")]
    public DateTime LastLoggedInAt { get; set; }

    [JsonPropertyName("last_activity_at")]
    public DateTime LastActivityAt { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

}
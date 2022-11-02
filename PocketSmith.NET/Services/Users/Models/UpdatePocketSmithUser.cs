using System.Text.Json.Serialization;

namespace PocketSmith.NET.Services.Users.Models;

public class UpdatePocketSmithUser
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("time_zone")]
    public string TimeZone { get; set; }

    [JsonPropertyName("week_start_day")]
    public short WeekStartDay { get; set; }

    [JsonPropertyName("beta_user")]
    public bool BetaUser { get; set; }

    [JsonPropertyName("base_currency_code")]
    public string BaseCurrencyCode { get; set; }

    [JsonPropertyName("always_show_base_currency")]
    public bool AlwaysShowBaseCurrency { get; set; }
}
using PocketSmith.NET.Models;
using System.Text.Json.Serialization;

namespace PocketSmith.NET.Services.Accounts.Models;

public class UpdatePocketSmithAccount
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; }

    [JsonIgnore]
    public PocketSmithAccountType Type { get; set; }

    [JsonPropertyName("is_net_worth")]
    public bool IsNetWorth { get; set; }
}
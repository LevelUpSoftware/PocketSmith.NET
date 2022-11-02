using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;
using PocketSmith.NET.JsonConverters;

namespace PocketSmith.NET.Models;

[HttpRoute("events")]
public class PocketSmithEvent
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("category")]
    public PocketSmithCategory Category { get; set; }

    [JsonPropertyName("scenario")]
    public PocketSmithAccountScenario Scenario { get; set; }

    [JsonPropertyName("amount")]
    public double Amount { get; set; }

    [JsonPropertyName("amount_in_base_currency")]
    public double AmountInBaseCurrency { get; set; }

    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; }
    
    [JsonConverter(typeof(DateTimeToShortDateJsonConverter))]
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("colour")]
    public string Colour { get; set; }

    [JsonPropertyName("note")]
    public string Note { get; set; }

    [JsonPropertyName("repeat_type")]
    public string RepeatType { get; set; }

    [JsonPropertyName("repeat_interval")]
    public int RepeatInterval { get; set; }

    [JsonPropertyName("series_id")]
    public long SeriesId { get; set; }

    [JsonPropertyName("series_start_id")]
    public string SeriesStartId { get; set; }

    [JsonPropertyName("infinite_series")]
    public bool InfiniteSeries { get; set; }

    [JsonPropertyName("is_transfer")]
    public bool IsTransfer { get; set; }
}
using PocketSmith.NET.JsonConverters;
using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;

namespace PocketSmith.NET.Models;

[HttpRoute("budget_summary")]
public class PocketSmithBudgetEvent
{
    [JsonConverter(typeof(DateTimeToShortDateJsonConverter))]
    [JsonPropertyName("start_date")]
    public DateOnly StartDate { get; set; }

    [JsonConverter(typeof(DateTimeToShortDateJsonConverter))]
    [JsonPropertyName("end_date")]
    public DateOnly EndDate { get; set; }

    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; }

    [JsonPropertyName("total_actual_amount")]
    public double TotalActualAmount { get; set; }

    [JsonPropertyName("average_actual_amount")]
    public double AverageActualAmount { get; set; }

    [JsonPropertyName("total_forecast_amount")]
    public double TotalForecastAmount { get; set; }

    [JsonPropertyName("average_forecast_amount")]
    public double AverageForecastAmount { get; set; }

    [JsonPropertyName("total_under_by")]
    public double TotalUnderBy { get; set; }

    [JsonPropertyName("total_over_by")]
    public double TotalOverBy { get; set; }

    [JsonPropertyName("periods")]
    public List<PocketSmithBudgetEventPeriod> Periods { get; set; }


}
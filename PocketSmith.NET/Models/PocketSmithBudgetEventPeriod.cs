﻿using System.Text.Json.Serialization;
using PocketSmith.NET.JsonConverters;

namespace PocketSmith.NET.Models;

public class PocketSmithBudgetEventPeriod
{
    [JsonConverter(typeof(DateTimeToShortDateJsonConverter))]
    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }

    [JsonConverter(typeof(DateTimeToShortDateJsonConverter))]
    [JsonPropertyName("end_date")]
    public DateTime EndDate { get; set; }

    [JsonPropertyName("currency_code")]
    public double CurrencyCode { get; set; }

    [JsonPropertyName("actual_amount")]
    public double ActualAmount { get; set; }

    [JsonPropertyName("forecast_amount")]
    public double ForecastAmount { get; set; }

    [JsonPropertyName("refund_amount")]
    public double RefundAmount { get; set; }

    [JsonPropertyName("current")]
    public bool Current { get; set; }

    [JsonPropertyName("over_budget")]
    public bool OverBudget { get; set; }

    [JsonPropertyName("under_budget")]
    public bool UnderBudget { get; set; }

    [JsonPropertyName("over_by")]
    public double OverBy { get; set; }
    
    [JsonPropertyName("under_by")]
    public double UnderBy { get; set; }

    [JsonPropertyName("percentage_used")]
    public double PercentageUsed { get; set; }
}
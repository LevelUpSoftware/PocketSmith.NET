using PocketSmith.NET.JsonConverters;
using System.Text.Json.Serialization;
using PocketSmith.NET.Attributes;

namespace PocketSmith.NET.Models;

[HttpRoute("transactions")]
public class PocketSmithTransaction
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("payee")]
    public string Payee { get; set; }

    [JsonPropertyName("original_payee")]
    public string OriginalPayee { get; set; }

    [JsonConverter(typeof(DateOnlyToShortDateStringConverter))]
    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("upload_source")]
    public string UploadSource { get; set; }

    [JsonPropertyName("category")] 
    public PocketSmithCategory Category { get; set; }

    [JsonPropertyName("closing_balance")]
    public double ClosingBalance { get; set; }

    [JsonPropertyName("cheque_number")]
    public string ChequeNumber { get; set; }

    [JsonPropertyName("memo")]
    public string Memo { get; set; }

    [JsonPropertyName("amount")]
    public double Amount { get; set; }

    [JsonPropertyName("amount_in_base_currency")]
    public double AmountInBaseCurrency { get; set; }

    [JsonConverter(typeof(TransactionTypeJsonConverter))]
    [JsonPropertyName("type")]
    public PocketSmithTransactionType Type { get; set; }

    [JsonPropertyName("is_transfer")]
    public bool? IsTransfer { get; set; }

    [JsonPropertyName("needs_review")]
    public bool? NeedsReview { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("note")]
    public string Note { get; set; }

    [JsonPropertyName("labels")]
    public List<string> Labels { get; set; }

    [JsonPropertyName("transaction_account")]
    public PocketSmithTransactionAccount TransactionAccount { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpDateTime { get; set; }

    [JsonIgnore]
    public int PageNumber { get; set; }

    [JsonIgnore]
    public int TotalPages { get; set; }
}
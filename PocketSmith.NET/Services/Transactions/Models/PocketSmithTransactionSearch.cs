using System.Text.Json.Serialization;
using PocketSmith.NET.JsonConverters;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Transactions;

public class PocketSmithTransactionSearch
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public DateOnly UpdatedSince { get; set; }
    public bool Uncategorized { get; set; }
    public PocketSmithTransactionType Type { get; set; }
    public bool NeedsReview { get; set; }
    public string Search { get; set; }
}
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Transactions;

public class PocketSmithTransactionSearch
{
    public PocketSmithTransactionSearch()
    {
        TransactionsPerPage = 30;
    }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public DateTime? UpdatedSince { get; set; }
    public bool? Uncategorized { get; set; }
    public PocketSmithTransactionType? Type { get; set; }
    public bool? NeedsReview { get; set; }
    public string? Search { get; set; }
    public int TransactionsPerPage { get; set; }
}
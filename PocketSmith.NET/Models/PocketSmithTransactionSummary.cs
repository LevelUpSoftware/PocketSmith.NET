namespace PocketSmith.NET.Models;

public class PocketSmithTransactionSummary
{
    public PocketSmithTransactionSummary()
    {
        Transactions = new List<PocketSmithTransaction>();
    }
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public List<PocketSmithTransaction> Transactions { get; set; }
}
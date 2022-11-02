namespace PocketSmith.NET.Services.Transactions;

public class UpdatePocketSmithTransaction
{
    public string Memo { get; set; }
    public string CheckNumber { get; set; }
    public string Payee { get; set; }
    public double Amount { get; set; }
    public DateOnly Date { get; set; }
    public bool IsTransfer { get; set; }
    public int CategoryId { get; set; }
    public string Note { get; set; }
    public bool NeedsReview { get; set; }
    public List<string> Labels { get; set; }
}
namespace PocketSmith.NET.Services.Transactions.Models;

public class CreatePocketSmithTransaction
{
    public CreatePocketSmithTransaction()
    {
        Labels = new List<string>();
    }
    public int TransactionAccountId { get; set; }
    public string Payee { get; set; }
    public double Amount { get; set; }
    public DateOnly Date { get; set; }
    public bool? IsTransfer { get; set; }
    public List<string> Labels { get; set; }
    public int? CategoryId { get; set; }
    public string? Note { get; set; }
    public string? Memo { get; set; }
    public int? CheckNumber { get; set; }
    public bool? NeedsReview { get; set; }
}
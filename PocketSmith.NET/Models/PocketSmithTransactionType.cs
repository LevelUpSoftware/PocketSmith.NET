using System.ComponentModel.DataAnnotations;

namespace PocketSmith.NET.Models;

public enum PocketSmithTransactionType
{
    [Display(Name = "debit")]
    Debit,
    [Display(Name = "credit")]
    Credit
}
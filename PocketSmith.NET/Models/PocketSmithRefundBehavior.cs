using System.ComponentModel.DataAnnotations;

namespace PocketSmith.NET.Models;

public enum PocketSmithRefundBehavior
{
    [Display(Name = "debits_are_deductions")]
    DebitsAreDeductions,
    [Display(Name = "credits_are_refunds")]
    CreditsAreRefunds
}
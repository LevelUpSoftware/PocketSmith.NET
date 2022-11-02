using System.ComponentModel.DataAnnotations;

namespace PocketSmith.NET.Services.Budgets.Models;

public enum BudgetEventPeriod
{
    [Display(Name = "weeks")]
    Weeks,
    [Display(Name = "months")]
    Months,
    [Display(Name = "years")]
    Years,
    [Display(Name = "event")]
    Event
}
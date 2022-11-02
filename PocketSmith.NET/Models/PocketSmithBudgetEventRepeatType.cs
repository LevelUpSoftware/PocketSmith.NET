using System.ComponentModel.DataAnnotations;

namespace PocketSmith.NET.Models;

public enum PocketSmithBudgetEventRepeatType
{
    [Display(Name = "once")]
    Once,
    [Display(Name = "daily")]
    Daily,
    [Display(Name = "weekly")]
    Weekly,
    [Display(Name = "fortnightly")]
    Fortnightly,
    [Display(Name = "monthly")]
    Monthly,
    [Display(Name = "yearly")]
    Yearly,
    [Display(Name = "each weekday")]
    EachWeekday
}
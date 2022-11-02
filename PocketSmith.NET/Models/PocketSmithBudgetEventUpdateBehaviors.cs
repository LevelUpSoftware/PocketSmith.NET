using System.ComponentModel.DataAnnotations;

namespace PocketSmith.NET.Models;

public enum PocketSmithBudgetEventUpdateBehaviors
{
    [Display(Name = "one")]
    One,
    [Display(Name = "forward")]
    Forward,
    [Display(Name = "all")]
    All
}
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Events.Models;

public class UpdatePocketSmithEvent
{
    public PocketSmithBudgetEventUpdateBehaviors Behavior { get; set; }
    public double? Amount { get; set; }
    public PocketSmithBudgetEventRepeatType RepeatType { get; set; }
    public int RepeatInterval { get; set; }
    public string? Note { get; set; }
}
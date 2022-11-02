using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Events.Models;

public class CreatePocketSmithEvent
{
    public int ScenarioId { get; set; }
    public int CategoryId { get; set; }
    public DateOnly Date { get; set; }
    public double Amount { get; set; }
    public PocketSmithBudgetEventRepeatType RepeatType { get; set; }
    public int? RepeatInterval { get; set; }
    public string? Note { get; set; }
}
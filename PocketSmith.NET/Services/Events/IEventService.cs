using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Events.Models;

namespace PocketSmith.NET.Services.Events;

public interface IEventService
{
    Task<PocketSmithEvent?> GetByIdAsync(string id);
    Task<PocketSmithEvent> UpdateAsync(UpdatePocketSmithEvent updateItem, string id);
    Task DeleteAsync(string id);
    Task<IEnumerable<PocketSmithEvent>> GetAllAsync(DateOnly startDate, DateOnly endDate);
    Task<IEnumerable<PocketSmithEvent>> GetAllByScenarioIdAsync(int scenarioId, DateOnly startDate, DateOnly endDate);
    Task<PocketSmithEvent> CreateAsync(CreatePocketSmithEvent createItem);
}
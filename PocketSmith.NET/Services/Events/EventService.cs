using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Events.Models;

namespace PocketSmith.NET.Services.Events;

public class EventService : ServiceBase<PocketSmithEvent, string>, IEventService
{
    public EventService(int userId, string apiKey) : base(userId, apiKey)
    {
    }

    public virtual async Task<PocketSmithEvent> CreateAsync(CreatePocketSmithEvent createItem)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithAccountScenario))
            .AddRoute(createItem.ScenarioId.ToString())
            .AddRouteFromModel(typeof(PocketSmithEvent))
            .Uri;

        var request = new
        {
            category_id = createItem.CategoryId,
            date = createItem.Date.ToString("yyyy-MM-dd"),
            amount = createItem.Amount,
            repeat_type = createItem.RepeatType.GetDisplayName(),
            repeat_interval = createItem.RepeatInterval,
            note = createItem.Note
        };

        var response = await ApiHelper.PostAsync<PocketSmithEvent>(uri, request);
        return response;
    }

    public virtual async Task DeleteAsync(string id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithEvent))
            .AddRoute(id)
            .Uri;

        await ApiHelper.DeleteAsync(uri);
    }

    public virtual async Task<IEnumerable<PocketSmithEvent>> GetAllByScenarioIdAsync(int scenarioId, DateOnly startDate, DateOnly endDate)
    {
        var uri = UriBuilder.AddRouteFromModel(typeof(PocketSmithAccountScenario))
            .AddRoute(scenarioId.ToString())
            .AddRouteFromModel(typeof(PocketSmithEvent))
            .AddQuery("start_date", startDate.ToString("yyyy-MM-dd"))
            .AddQuery("end_date", endDate.ToString("yyyy-MM-dd"))
            .Uri;

        var response = await ApiHelper.GetAsync<IEnumerable<PocketSmithEvent>>(uri);
        return response;
    }

    public virtual async Task<IEnumerable<PocketSmithEvent>> GetAllAsync(DateOnly startDate, DateOnly endDate)
    {
        var uri = UriBuilder.AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithEvent))
            .AddQuery("start_date", startDate.ToString("yyyy-MM-dd"))
            .AddQuery("end_date", endDate.ToString("yyyy-MM-dd"))
            .Uri;

        var response = await ApiHelper.GetAsync<IEnumerable<PocketSmithEvent>>(uri);
        return response;
    }

    public virtual async Task<PocketSmithEvent> UpdateAsync(UpdatePocketSmithEvent updateItem, string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id));
        }

        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithEvent))
            .AddRoute(id)
            .Uri;

        var request = new
        {
            behaviour = updateItem.Behavior.GetDisplayName(),
            amount = updateItem.Amount,
            repeat_type = updateItem.RepeatType.GetDisplayName(),
            repeat_interval = updateItem.RepeatInterval,
            note = updateItem.Note
        };

        var response = await ApiHelper.PutAsync<PocketSmithEvent>(uri, updateItem);
        return response;
    }
}
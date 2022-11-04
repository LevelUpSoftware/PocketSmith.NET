using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Budgets.Models;

namespace PocketSmith.NET.Services.Budgets;

public class BudgetService : ServiceBase<PocketSmithBudget, int>, IBudgetService
{
    public BudgetService(int userId, string apiKey) : base(userId, apiKey)
    {
    }

    public virtual async Task DeleteForecastCacheAsync()
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRoute("forecast_cache")
            .GetUriAndReset();

        await ApiHelper.DeleteAsync(uri);
    }

    public virtual async Task<IEnumerable<PocketSmithBudget>> GetAllAsync(bool rollUp = false)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithBudget))
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<List<PocketSmithBudget>>(uri);
        return response;
    }

    public virtual async Task<PocketSmithBudgetEvent> GetBudgetSummaryAsync(BudgetEventPeriod period, int periodInterval, DateOnly startDate, DateOnly endDate)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithBudgetEvent))
            .AddQuery("period", period.GetDisplayName())
            .AddQuery("interval", periodInterval.ToString())
            .AddQuery("start_date", startDate.ToString("yyyy-MM-dd"))
            .AddQuery("end_date", endDate.ToString("yyyy-MM-dd"))
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<PocketSmithBudgetEvent>(uri);
        return response;
    }

    public virtual async Task<PocketSmithBudgetEvent> GetTrendAnalysisAsync(BudgetEventPeriod period, int periodInterval, DateOnly startDate, DateOnly endDate, List<int> categoryIds, List<int> scenarioIds)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRoute("trend_analysis")
            .AddQuery("period", period.GetDisplayName())
            .AddQuery("interval", periodInterval.ToString())
            .AddQuery("start_date", startDate.ToString("yyyy-MM-dd"))
            .AddQuery("end_date", endDate.ToString("yyyy-MM-dd"))
            .AddQuery("categories", string.Join(",", categoryIds))
            .AddQuery("scenarios", string.Join(",", scenarioIds))
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<PocketSmithBudgetEvent>(uri);
        return response;
    }
}
using Microsoft.Extensions.Configuration;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Budgets.Models;

namespace PocketSmith.NET.Services.Budgets;

public class BudgetService : ServiceBase<PocketSmithBudget, int>, IBudgetService, IPocketSmithService
{
    public BudgetService(IApiHelper apiHelper, IConfiguration configuration) : base(apiHelper, configuration)
    {
    }
    public BudgetService(IApiHelper apiHelper, int userId, string apiKey) : base(apiHelper, userId, apiKey)
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

    public virtual async Task<IList<PocketSmithBudget>> GetAllAsync(bool rollUp = false)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithBudget))
            .AddQuery("roll_up", rollUp.ToString().ToLower())
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<List<PocketSmithBudget>>(uri);
        return response ?? new List<PocketSmithBudget>();
    }

    public virtual async Task<PocketSmithBudgetEvent?> GetBudgetSummaryAsync(BudgetEventPeriod period, int periodInterval, DateOnly startDate, DateOnly endDate)
    {
        validateDates(startDate, endDate);

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

    public virtual async Task<PocketSmithBudgetEvent?> GetTrendAnalysisAsync(BudgetEventPeriod period, int periodInterval, DateOnly startDate, DateOnly endDate, List<int> categoryIds, List<int> scenarioIds)
    {
        validateDates(startDate, endDate);
        validateCategoriesandScenarios(categoryIds, scenarioIds);

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

    private void validateDates(DateOnly startDate, DateOnly endDate)
    {
        if (startDate > endDate)
        {
            throw new PocketSmithValidationException($"The specified start date {startDate} occurs after the specified end date {endDate}");
        }
    }

    private void validateCategoriesandScenarios(List<int> categoryIds, List<int> scenarioIds)
    {
        if (!categoryIds.Any())
        {
            throw new PocketSmithValidationException("At lease one categoryId is required.");
        }

        if (categoryIds.Any(x => x < 1))
        {
            throw new PocketSmithValidationException($"Argument {nameof(categoryIds)} contains an invalid categoryId.");
        }

        if (!scenarioIds.Any())
        {
            throw new PocketSmithValidationException("At least one scenarioId is required.");
        }

        if (scenarioIds.Any(x => x < 1))
        {
            throw new PocketSmithValidationException($"Arguemnt {nameof(scenarioIds)} contains an invalid scenarioId");
        }
    }
}
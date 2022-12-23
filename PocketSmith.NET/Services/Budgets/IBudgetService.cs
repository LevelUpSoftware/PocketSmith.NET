using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Budgets.Models;

namespace PocketSmith.NET.Services.Budgets;

public interface IBudgetService
{
    Task<IList<PocketSmithBudget>> GetAllAsync(bool rollUp = false);

    Task<PocketSmithBudgetEvent?> GetBudgetSummaryAsync(BudgetEventPeriod period,
        int periodInterval, DateOnly startDate, DateOnly endDate);

    Task<PocketSmithBudgetEvent?> GetTrendAnalysisAsync(BudgetEventPeriod period,
        int periodInterval, DateOnly startDate, DateOnly endDate, List<int> categoryIds, List<int> scenarioIds);

    Task DeleteForecastCacheAsync();
}
using System.Formats.Asn1;
using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Budgets;
using PocketSmith.NET.Services.Budgets.Models;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class BudgetServiceTests
{
    private IBudgetService _budgetService;
    private string _currentUri;

    [TestInitialize]
    public void Init()
    {
        _budgetService = new BudgetService(setupIApiHelper(), 1, "apiKey");
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/budget?roll_up=true";

        _ = await _budgetService.GetAllAsync(true);
        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task GetBudgetSummaryAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/budget_summary?period=weeks&interval=1&start_date=2022-01-01&end_date=2022-12-31";

        _ = await _budgetService.GetBudgetSummaryAsync(BudgetEventPeriod.Weeks, 1,
            new DateOnly(2022, 01, 01), new DateOnly(2022, 12, 31));
        
        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task GetBudgetSummaryAsync_Failed_StartDateOccursAfterEndDate()
    {
        _ = await _budgetService.GetBudgetSummaryAsync(BudgetEventPeriod.Weeks, 1, new DateOnly(2022, 3, 1),
            new DateOnly(2021, 12, 2));
    }

    [TestMethod]
    public async Task GetTrendAnalysisAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/trend_analysis?period=weeks&interval=1&start_date=2022-01-01&end_date=2022-12-31&categories=1%2C2&scenarios=25%2C26";

        _ = await _budgetService.GetTrendAnalysisAsync(BudgetEventPeriod.Weeks, 1, new DateOnly(2022, 1, 1),
            new DateOnly(2022, 12, 31), new List<int>{1,2}, new List<int>{25,26});

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task GetTrendAnalysisAsync_Failed_StartDateOccursAfterEndDate()
    {
        _ = await _budgetService.GetTrendAnalysisAsync(BudgetEventPeriod.Weeks, 2, new DateOnly(2022, 3, 1),
            new DateOnly(2021, 11, 3), new List<int> { 1, 2 }, new List<int> { 2, 3 });
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task GetTrendAnalysisAsync_Failed_NoCategoryId()
    {
        _ = await _budgetService.GetTrendAnalysisAsync(BudgetEventPeriod.Years, 5, new DateOnly(2015, 1, 1),
            new DateOnly(2020, 12, 31), new List<int>(), new List<int> { 45, 46 });
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task GetTrendAnalysisAsync_Failed_InvalidCategoryId()
    {
        _ = await _budgetService.GetTrendAnalysisAsync(BudgetEventPeriod.Months, 2, new DateOnly(2022, 1, 1),
            new DateOnly(2022, 12, 31), new List<int> { 0 }, new List<int> { 71, 72 });
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task GetTrendAnalysisAsync_Failed_NoScenarioId()
    {
        _ = await _budgetService.GetTrendAnalysisAsync(BudgetEventPeriod.Event, 1, new DateOnly(2022, 01, 01),
            new DateOnly(2022, 12, 31), new List<int> { 45, 48 }, new List<int>());
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task GetTrendAnalysisAsync_Failed_InvalidScenarioId()
    {
        _ = await _budgetService.GetTrendAnalysisAsync(BudgetEventPeriod.Weeks, 4, new DateOnly(2022, 1, 1),
            new DateOnly(2022, 12, 31), new List<int> { 87, 80 }, new List<int> { 0 });
    }

    [TestMethod]
    public async Task DeleteForecastCacheAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/forecast_cache";

        await _budgetService.DeleteForecastCacheAsync();
        
        Assert.AreEqual(expectedUri, _currentUri);
    }

    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

        moqApiHelper.Setup(x => x.PostAsync<PocketSmithBudget>(It.IsAny<Uri>(), It.IsAny<object>())).ReturnsAsync(
            (Uri uri, object requestObject) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<PocketSmithBudget>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<PocketSmithBudgetEvent>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });


        moqApiHelper.Setup(x => x.GetAsync<List<PocketSmithBudget>>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.PutAsync<PocketSmithBudget>(It.IsAny<Uri>(), It.IsAny<object>()))
            .ReturnsAsync((Uri uri, object requestObject) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.DeleteAsync(It.IsAny<Uri>()))
            .Returns((Uri uri) =>
            {
                _currentUri = uri.ToString();
                return Task.FromResult("");
            });

        return moqApiHelper.Object;
    }

}
using System.Formats.Asn1;
using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Events;
using PocketSmith.NET.Services.Events.Models;
using PocketSmith.NET.Services.Events.Validators;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class EventServiceTests
{
    private IEventService _eventService;
    private string _currentUri;

    [TestInitialize]
    public void Init()
    {
        _eventService = new EventService(setupIApiHelper(), 1, "apiKey", new CreateEventValidator());
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/events?start_date=2022-01-01&end_date=2022-12-31";

        _ = await _eventService.GetAllAsync(new DateOnly(2022, 01, 01), new DateOnly(2022, 12, 31));

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task GetAllByScenarioIdAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/scenarios/22/events?start_date=2022-01-01&end_date=2022-12-31";

        _ = await _eventService.GetAllByScenarioIdAsync(22, new DateOnly(2022, 1, 1), new DateOnly(2022, 12, 31));

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task GetByIdAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/events/23";

        _ = await _eventService.GetByIdAsync("23");

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task CreateAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/scenarios/45/events";
        _ = await _eventService.CreateAsync(new CreatePocketSmithEvent
        {
            CategoryId = 1,
            Amount = 2500.25,
            Date = new DateOnly(2022, 01, 01),
            RepeatInterval = 1,
            RepeatType = PocketSmithBudgetEventRepeatType.Monthly,
            ScenarioId = 45
        });

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidCategoryId()
    {
        _ = await _eventService.CreateAsync(new CreatePocketSmithEvent
        {
            ScenarioId = 77,
            Amount = 25.00,
            Date = new DateOnly(2022, 01, 01),
            RepeatType = PocketSmithBudgetEventRepeatType.Once
        });
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidScenarioId()
    {
        _ = await _eventService.CreateAsync(new CreatePocketSmithEvent
        {
            CategoryId = 77,
            Amount = 25.00,
            Date = new DateOnly(2022, 01, 01),
            RepeatType = PocketSmithBudgetEventRepeatType.Once

        });
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidAmount()
    {
        _ = await _eventService.CreateAsync(new CreatePocketSmithEvent
        {
            CategoryId = 77,
            ScenarioId = 77,
            Date = new DateOnly(2022, 01, 01)
        });
    }

    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

        moqApiHelper.Setup(x => x.PostAsync<PocketSmithEvent>(It.IsAny<Uri>(), It.IsAny<object>())).ReturnsAsync(
            (Uri uri, object requestObject) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<PocketSmithEvent>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<List<PocketSmithEvent>>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.PutAsync<PocketSmithEvent>(It.IsAny<Uri>(), It.IsAny<object>()))
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
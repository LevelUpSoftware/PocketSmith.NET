using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.TimeZones;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class TimeZoneServiceTests
{
    private ITimeZoneService _timeZoneService;
    private string _currentUri;

    [TestInitialize]
    public void Init()
    {
        _timeZoneService = new TimeZoneService(setupIApiHelper(), 1, "apiKey");
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/time_zones";

        _ = await _timeZoneService.GetAllAsync();

        Assert.IsTrue(_currentUri == expectedUri);
    }

    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

        moqApiHelper.Setup(x => x.GetAsync<List<PocketSmithTimeZone>>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });
        
        return moqApiHelper.Object;
    }
}
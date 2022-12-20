using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Currencies;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class CurrencyServiceTests
{
    private ICurrencyService _currencyService;
    private string _currentUri;

    [TestInitialize]
    public void Init()
    {
        _currencyService = new CurrencyService(setupIApiHelper(), 1, "apiKey");
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/currencies";

        _ = await _currencyService.GetAllAsync();

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task GetByIdAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/currencies/22";

        _ = await _currencyService.GetByIdAsync("22");

        Assert.AreEqual(expectedUri, _currentUri);
    }

    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

        moqApiHelper.Setup(x => x.GetAsync<PocketSmithCurrency>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<List<PocketSmithCurrency>>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        return moqApiHelper.Object;
    }

}
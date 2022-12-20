using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Accounts;
using PocketSmith.NET.Services.Accounts.Models;
using PocketSmith.NET.Services.Accounts.Validators;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class AccountServiceTests
{
    private IAccountService _accountService;
    private string _currentUri;

    [TestInitialize]
    public void Init()
    {
        _accountService = new AccountService(setupIApiHelper(), 1, "apiKey", new CreateAccountValidator());
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/accounts";

        _ = await _accountService.GetAllAsync();

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task GetByIdAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/accounts/22";

        _ = await _accountService.GetByIdAsync(22);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task GetAllByInstitutionIdAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/institutions/87/accounts";

        _ = await _accountService.GetAllByInstitutionIdAsync(87);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task DeleteAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/accounts/1001";

        await _accountService.DeleteAsync(1001);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task CreateAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/accounts";

        _ = await _accountService.CreateAsync(new CreatePocketSmithAccount
        {
            CurrencyCode = "usd",
            InstitutionId = 1,
            Title = "Test Account",
            Type = PocketSmithAccountType.Bank
        });

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidInstitutionId()
    {
        await _accountService.CreateAsync(new CreatePocketSmithAccount
        {
            CurrencyCode = "usd",
            Title = "Test Account",
            Type = PocketSmithAccountType.Bank
        });
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidCurrencyCode()
    {
        await _accountService.CreateAsync(new CreatePocketSmithAccount
        {
            InstitutionId = 25,
            Title = "Test Account",
            Type = PocketSmithAccountType.Bank
        });
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidTitle()
    {
        await _accountService.CreateAsync(new CreatePocketSmithAccount
        {
            InstitutionId = 25,
            CurrencyCode = "usd",
            Type = PocketSmithAccountType.Bank
        });
    }

    [TestMethod]
    public async Task UpdateAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/accounts/8761";

        _ = await _accountService.UpdateAsync(new UpdatePocketSmithAccount(), 8761);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task UpdateDisplayOrderAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/accounts";

        await _accountService.UpdateDisplayOrder(new List<int> { 1, 2 });

        Assert.AreEqual(expectedUri, _currentUri);
    }

    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

        moqApiHelper.Setup(x => x.PostAsync<PocketSmithAccount>(It.IsAny<Uri>(), It.IsAny<object>())).ReturnsAsync(
            (Uri uri, object requestObject) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<PocketSmithAccount>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<List<PocketSmithAccount>>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.PutAsync<PocketSmithAccount>(It.IsAny<Uri>(), It.IsAny<object>()))
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
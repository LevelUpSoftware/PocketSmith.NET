using System.Formats.Asn1;
using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Institutions;
using PocketSmith.NET.Services.Institutions.Models;
using PocketSmith.NET.Services.Institutions.Validators;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class InstitutionServiceTests
{
    private IInstitutionService _institutionService;
    private string _currentUri;

    [TestInitialize]
    public void Init()
    {
        _institutionService = new InstitutionService(setupIApiHelper(), 1, "apiKey", new CreateInstitutionValidator());
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/institutions";

        _ = await _institutionService.GetAllAsync();

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task GetByIdAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/institutions/44";

        _ = await _institutionService.GetByIdAsync(44);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task DeleteAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/institutions/44";
        await _institutionService.DeleteAsync(44);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task CreateAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/institutions";
        _ = await _institutionService.CreateAsync(new CreatePocketSmithInstitution
        {
            Title = "Jellystone National Bank",
            CurrencyCode = "usd"
        });

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidTitle()
    {
        _ = await _institutionService.CreateAsync(new CreatePocketSmithInstitution
        {
            Title = "",
            CurrencyCode = "usd"
        });
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_Invalid_Currency_Code()
    {
        _ = await _institutionService.CreateAsync(new CreatePocketSmithInstitution
        {
            Title = "Jellystone National Bank",
            CurrencyCode = ""
        });
    }

    [TestMethod]
    public async Task UpdateAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/institutions/44";
        _ = await _institutionService.UpdateAsync(new UpdatePocketSmithInstitution
        {
            Title = "First National Bank of Jellystone"
        }, 44);

        Assert.AreEqual(expectedUri, _currentUri);
    }


    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

        moqApiHelper.Setup(x => x.PostAsync<PocketSmithInstitution>(It.IsAny<Uri>(), It.IsAny<object>())).ReturnsAsync(
            (Uri uri, object requestObject) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<PocketSmithInstitution>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<List<PocketSmithInstitution>>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.PutAsync<PocketSmithInstitution>(It.IsAny<Uri>(), It.IsAny<object>()))
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
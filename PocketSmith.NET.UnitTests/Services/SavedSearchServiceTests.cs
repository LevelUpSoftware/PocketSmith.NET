using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Factories;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.SavedSearches;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class SavedSearchServiceTests
{
    private ISavedSearchService _service;
    private string _currentUri;

    [TestInitialize]
    public void Initialize()
    {
        _service = new SavedSearchService(setupIApiHelper(), 1, "apiKey");
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/saved_searches";

        var _ = await _service.GetAllAsync();

        Assert.IsTrue(_currentUri == expectedUri);
    }

    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

        moqApiHelper.Setup(x => x.PostAsync<PocketSmithSavedSearch>(It.IsAny<Uri>(), It.IsAny<object>())).ReturnsAsync(
            (Uri uri, object requestObject) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<PocketSmithSavedSearch>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<List<PocketSmithSavedSearch>>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.PutAsync<PocketSmithSavedSearch>(It.IsAny<Uri>(), It.IsAny<object>()))
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
using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Services.Labels;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class LabelServiceTests
{
    private ILabelService _labelService;
    private string _currentUri;

    [TestInitialize]
    public void Initialize()
    {
        _labelService = new LabelService(setupIApiHelper(), 1, "apiKey");
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/labels";
        _ = await _labelService.GetAllAsync();

        Assert.IsTrue(_currentUri == expectedUri);
    }

    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

       
        moqApiHelper.Setup(x => x.GetAsync<List<string>>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

      
        return moqApiHelper.Object;
    }

}
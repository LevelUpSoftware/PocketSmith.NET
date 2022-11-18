using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Users;
using PocketSmith.NET.Services.Users.Models;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class UserServiceTests
{
    private UserService _userService;
    private string _currentUri;

    [TestInitialize]
    public void Initialize()
    {
        _userService = new UserService(setupIApiHelper(), 1, "apiKey");
    }

    [TestMethod]
    public async Task GetAuthorizedUserAsync_Success()
    {
        string expectedUri = "https://api.pocketsmith.com/v2/me";

        _ = await _userService.GetAuthorizedUserAsync();

        Assert.IsTrue(_currentUri == expectedUri);
    }

    [TestMethod]
    public async Task GetAsync_Success()
    {
        string expectedUri = "https://api.pocketsmith.com/v2/users/1";

        _ = await _userService.GetAsync();
        Assert.IsTrue(_currentUri == expectedUri);
    }

    [TestMethod]
    public async Task UpdateAsync_Success()
    {
        string expectedUri = "https://api.pocketsmith.com/v2/users/1";

        var updateItem = new UpdatePocketSmithUser();
        _ = await _userService.UpdateAsync(updateItem);

        Assert.IsTrue(_currentUri == expectedUri);
    }


    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();
        moqApiHelper.Setup(x => x.PostAsync<PocketSmithUser>(It.IsAny<Uri>(), It.IsAny<object>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<PocketSmithUser>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.PutAsync<PocketSmithUser>(It.IsAny<Uri>(), It.IsAny<object>()))
            .ReturnsAsync((Uri uri, object requestObject) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        return moqApiHelper.Object;
    }
}
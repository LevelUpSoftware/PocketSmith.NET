using System.Formats.Asn1;
using System.Security.Cryptography.X509Certificates;
using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Categories;
using PocketSmith.NET.Services.Categories.Models;
using PocketSmith.NET.Services.Categories.Options;
using PocketSmith.NET.Services.Categories.Validators;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class CategoryServiceTests
{
    private ICategoryService _categoryService;
    private string _currentUri;

    [TestInitialize]
    public void Init()
    {
        _categoryService = new CategoryService(setupIApiHelper(), 1, "apiKey", new CreateCategoryValidator());
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/categories";

        _ = await _categoryService.GetAllAsync();

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task GetByIdAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/categories/72";

        _ = await _categoryService.GetByIdAsync(72);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task CreateAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/categories";

        _ = await _categoryService.CreateAsync(new CreatePocketSmithCategory
        {
            Title = "Picnic Baskets"
        });

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task UpdateAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/categories/67";

        _ = await _categoryService.UpdateAsync(new UpdatePocketSmithCategory
        {
            Title = "Ranger Salary"
        }, 67);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task DeleteAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/categories/76";

        await _categoryService.DeleteAsync(76);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidTitle()
    {
        _ = await _categoryService.CreateAsync(new CreatePocketSmithCategory
        {
            Title = ""
        });
    }

    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

        moqApiHelper.Setup(x => x.PostAsync<PocketSmithCategory>(It.IsAny<Uri>(), It.IsAny<object>())).ReturnsAsync(
            (Uri uri, object requestObject) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<PocketSmithCategory>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<List<PocketSmithCategory>>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.PutAsync<PocketSmithCategory>(It.IsAny<Uri>(), It.IsAny<object>()))
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
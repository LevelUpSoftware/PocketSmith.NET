using System.Formats.Asn1;
using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.CategoryRules;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class CategoryRulesServiceTests
{
    private ICategoryRuleService _categoryRuleService;
    private string _currentUri;

    [TestInitialize]
    public void Init()
    {
        _categoryRuleService = new CategoryRuleService(setupIApiHelper(), 1, "apiKey");
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/category_rules";

        _ = await _categoryRuleService.GetAllAsync();

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task CreateAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/categories/251/category_rules";

        _ = await _categoryRuleService.CreateAsync(251, "Jellystone Park");

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidPayee()
    {
        _ = await _categoryRuleService.CreateAsync(77, "");
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidCategoryId()
    {
        _ = await _categoryRuleService.CreateAsync(-2, "Jellystone Park");
    }

    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

        moqApiHelper.Setup(x => x.PostAsync<PocketSmithCategoryRule>(It.IsAny<Uri>(), It.IsAny<object>())).ReturnsAsync(
            (Uri uri, object requestObject) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<List<PocketSmithCategoryRule>>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        return moqApiHelper.Object;
    }

}
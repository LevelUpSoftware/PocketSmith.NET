using System.Runtime.CompilerServices;
using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Attachments;
using PocketSmith.NET.Services.Attachments.Models;
using PocketSmith.NET.Services.Attachments.Validators;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class AttachmentServiceTests
{
    private IAttachmentService _attachmentService;
    private string _currentUri;

    [TestInitialize]
    public void Init()
    {
        _attachmentService = new AttachmentService(setupIApiHelper(), 1, "apiKey", new CreateAttachmentValidator());
    }

    [TestMethod]
    public async Task GetByIdAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/attachments/86";

        _ = await _attachmentService.GetByIdAsync(86);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/attachments";

        _ = await _attachmentService.GetAllAsync();

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task GetAllByTransactionIdAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/transactions/147/attachments";

        _ = await _attachmentService.GetAllByTransactionIdAsync(147);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    public async Task CreateAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/attachments";

        _ = await _attachmentService.CreateAsync(new CreatePocketSmithAttachment
        {
            FileData = "Base64 File Contents=",
            FileName = "Test Receipt",
            Title = "Test Receipt"
        });

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidFile()
    {
        await _attachmentService.CreateAsync(new CreatePocketSmithAttachment
        {
            FileName = "Test File Name"
        });
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidFileName()
    {
        await _attachmentService.CreateAsync(new CreatePocketSmithAttachment
        {
            FileData = "Test File Data"
        });
    }

    [TestMethod]
    public async Task UpdateAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/attachments/3871";

        _ = await _attachmentService.UpdateAsync("UpdatedTitle", 3871);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public async Task UpdateAsync_Failed_InvalidTitle()
    {
        await _attachmentService.UpdateAsync(null, 1);
    }

    [TestMethod]
    public async Task DeleteAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/api/v2/attachments/8762";

        await _attachmentService.DeleteAsync(8762);
    }

    [TestMethod]
    public async Task AssignToTransactionAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/api/v2/transactions/8759/attachments";

        await _attachmentService.AssignToTransactionAsync(8759, 1);
    }

    [TestMethod]
    public async Task UnAssignFromTransactionAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/transactions/9871/attachments/17653";

        await _attachmentService.UnAssignFromTransactionAsync(9871, 17653);

        Assert.AreEqual(expectedUri, _currentUri);
    }

    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

        moqApiHelper.Setup(x => x.PostAsync<PocketSmithAttachment>(It.IsAny<Uri>(), It.IsAny<object>())).ReturnsAsync(
            (Uri uri, object requestObject) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<PocketSmithAttachment>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<List<PocketSmithAttachment>>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.PutAsync<PocketSmithAttachment>(It.IsAny<Uri>(), It.IsAny<object>()))
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
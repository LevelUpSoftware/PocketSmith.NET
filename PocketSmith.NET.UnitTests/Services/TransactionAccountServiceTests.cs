using System.Formats.Asn1;
using Moq;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.TransactionAccounts;
using PocketSmith.NET.Services.TransactionAccounts.Models;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class TransactionAccountServiceTests
{

    private TransactionAccountService _transactionAccountService;
    private string _currentUri;


    [TestInitialize]
    public void Init()
    {
        _transactionAccountService = new TransactionAccountService(setupIApiHelper(), 1, "apiKey");
    }

    [TestMethod]
    public async Task GetByIdAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/transaction_accounts/1";
        _ = await _transactionAccountService.GetByIdAsync(1);

        Assert.IsTrue(_currentUri == expectedUri);
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/users/1/transaction_accounts";
        _ = await _transactionAccountService.GetAllAsync();
        
        Assert.IsTrue(_currentUri == expectedUri);
    }

    [TestMethod]
    public async Task UpdateAsync_Success()
    {
        var expectedUri = "https://api.pocketsmith.com/v2/transaction_accounts/22";
        _ = await _transactionAccountService.UpdateAsync(new UpdatePocketSmithTransactionAccount(), 22);
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task UpdateAsync_Failed_InvalidId()
    {
        _ = await _transactionAccountService.UpdateAsync(new UpdatePocketSmithTransactionAccount(), 0);
    }

    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

        moqApiHelper.Setup(x => x.PostAsync<PocketSmithTransactionAccount>(It.IsAny<Uri>(), It.IsAny<object>())).ReturnsAsync(
            (Uri uri, object requestObject) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<PocketSmithTransactionAccount>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<List<PocketSmithTransactionAccount>>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.PutAsync<PocketSmithTransactionAccount>(It.IsAny<Uri>(), It.IsAny<object>()))
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
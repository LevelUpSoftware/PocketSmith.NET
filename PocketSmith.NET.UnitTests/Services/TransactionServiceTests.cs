using System.Formats.Asn1;
using Moq;
using Moq.Protected;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Transactions;
using System.Net;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Services.Transactions.Models;
using PocketSmith.NET.Services.Transactions.Validators;

namespace PocketSmith.NET.UnitTests.UriTests;

[TestClass]
public class TransactionServiceTests
{
    private TransactionService _transactionService;
    private string _currentUri;

    [TestInitialize]
    public void Initialize()
    {
        _transactionService = new TransactionService(setupIApiHelper(), 1, "apiKey", new CreateTransactionValidator());
    }

    [TestMethod]
    public async Task GetByIdAsync_Success()
    {
        string expectedUri = "https://api.pocketsmith.com/v2/transactions/1";
        
        _ = await _transactionService.GetByIdAsync(1);

        Assert.IsTrue(_currentUri == expectedUri);
    }

    [TestMethod]
    public async Task GetAllAsync_Success()
    {
        string expectedUri = "https://api.pocketsmith.com/v2/users/1/transactions";

        _ = await _transactionService.GetAllAsync();

        Assert.IsTrue(_currentUri == expectedUri);
    }

    [TestMethod]
    public async Task GetAllByAccountIdAsync_Success()
    {
        string expectedUri = "https://api.pocketsmith.com/v2/accounts/1/transactions";

        _ = await _transactionService.GetAllByAccountIdAsync(1);

        Assert.IsTrue(_currentUri == expectedUri);
    }

    [TestMethod]
    public async Task GetAllByTransactionAccountIdAsync_Success()
    {
        string expectedUri = "https://api.pocketsmith.com/v2/transaction_accounts/1/transactions";

        _ = await _transactionService.GetAllByTransactionAccountIdAsync(1);

        Assert.IsTrue(_currentUri == expectedUri);
    }

    [TestMethod]
    public async Task GetAllByCategoryIdAsync_Success()
    {
        string expectedUri = "https://api.pocketsmith.com/v2/categories/1/transactions";

        _ = await _transactionService.GetAllByCategoryIdAsync(1);

        Assert.IsTrue(_currentUri == expectedUri);
    }

    [TestMethod]
    public async Task CreateAsync_Success()
    {
        string expectedUri = "https://api.pocketsmith.com/v2/transaction_accounts/5500/transactions";

        _ = await _transactionService.CreateAsync(new CreatePocketSmithTransaction
        {
            Payee = "Jellystone Park",
            Amount = 200.00,
            CategoryId = 25,
            Date = new DateOnly(2022, 05, 01),
            TransactionAccountId = 5500
        });

        Assert.IsTrue(_currentUri == expectedUri);
    }

    [TestMethod]
    public async Task UpdateAsync_Success()
    {
        string expectedUri = "https://api.pocketsmith.com/v2/transactions/6001";
        _ = await _transactionService.UpdateAsync(new UpdatePocketSmithTransaction
        {
            Amount = 205.00
        }, 6001);

        Assert.IsTrue(_currentUri == expectedUri);
    }

    [TestMethod]
    public async Task DeleteAsync_Success()
    {
        string expectedUri = "https://api.pocketsmith.com/v2/transactions/6001";

        await _transactionService.DeleteAsync(6001);

        Assert.IsTrue(_currentUri == expectedUri);
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidPayee()
    {
        _ = await _transactionService.CreateAsync(new CreatePocketSmithTransaction
        {
            Payee = null,
            Amount = 10.00,
            CategoryId = 25,
            TransactionAccountId = 25,
            Date = DateOnly.FromDateTime(DateTime.Now)
        });
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidTransactionAccountId()
    {
        _ = await _transactionService.CreateAsync(new CreatePocketSmithTransaction
        {
            Payee = "Jellystone Park",
            Amount = 10.00,
            TransactionAccountId = 0,
            CategoryId = null
        });
    }

    [TestMethod]
    [ExpectedException(typeof(PocketSmithValidationException))]
    public async Task CreateAsync_Failed_InvalidCategoryId()
    {
        _ = await _transactionService.CreateAsync(new CreatePocketSmithTransaction
        {
            Payee = "Jellystone Park",
            Amount = 10.00,
            CategoryId = -1,
            Date = DateOnly.FromDateTime(DateTime.Now)
        });
    }
    private IApiHelper setupIApiHelper()
    {
        var moqApiHelper = new Mock<IApiHelper>();

        moqApiHelper.Setup(x => x.PostAsync<PocketSmithTransaction>(It.IsAny<Uri>(), It.IsAny<object>())).ReturnsAsync(
            (Uri uri, object requestObject) =>
            {
                _currentUri = uri.AbsoluteUri;
                return null;
            });

        moqApiHelper.Setup(x => x.GetAsync<PocketSmithTransaction>(It.IsAny<Uri>())).ReturnsAsync(
            (Uri uri) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.PutAsync<PocketSmithTransaction>(It.IsAny<Uri>(), It.IsAny<object>()))
            .ReturnsAsync((Uri uri, object requestObject) =>
            {
                _currentUri = uri.ToString();
                return null;
            });

        moqApiHelper.Setup(x => x.GetPagedAsync<PocketSmithTransaction>(It.IsAny<Uri>()))
            .ReturnsAsync((Uri uri) =>
            {
                _currentUri = uri.ToString();
                return new PocketSmithPagedQueryResult<PocketSmithTransaction>
                {
                    CurrentPage = 1,
                    TotalPages = 1,
                    Results = new List<PocketSmithTransaction>()
                };

            });

        moqApiHelper.Setup(x => x.DeleteAsync(It.IsAny<Uri>()))
            .Returns((Uri uri) =>
        {
            _currentUri = uri.ToString();
            return Task.FromResult("");
        });

        moqApiHelper.Setup(x => x.HttpClient).Returns(() =>
        {
            return setupHttpClient();
        });

        return moqApiHelper.Object;
    }

    private HttpClient setupHttpClient()
    {
        var httpMessageHandler = new Mock<HttpMessageHandler>();

        httpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).Returns(Task<HttpResponseMessage>.Factory.StartNew(
                () =>
                {
                    var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                    httpResponseMessage.Content = new StringContent("[]", System.Text.Encoding.UTF8, "application/json");
                    httpResponseMessage.Headers.Add("per-page", new List<string?>{"30"});
                    httpResponseMessage.Headers.Add("total", new List<string?>{"30"});
                    httpResponseMessage.Headers.Add("link", new List<string?>{ "<https://api.pocketsmith.com/v2/users/18609/transactions?end_date=2015-06-01&page=1&start_date=2011-01-01>; rel=\"first\", <https://api.pocketsmith.com/v2/users/18609/transactions?end_date=2015-06-01&page=12&start_date=2011-01-01>; rel=\"last\", <https://api.pocketsmith.com/v2/users/18609/transactions?end_date=2015-06-01&page=11&start_date=2011-01-01>; rel=\"next\", <https://api.pocketsmith.com/v2/users/18609/transactions?end_date=2015-06-01&page=9&start_date=2011-01-01>; rel=\"prev\">" });
                    return httpResponseMessage;
                }));

        var httpClient = new HttpClient(httpMessageHandler.Object);
        return httpClient;
    }

}
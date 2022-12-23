# PocketSmith.NET
A .NET Core API library for PocketSmith accounting suite. 

PocketSmith Website        | https://www.pocketsmith.com      
PocketSmith API Docs       | https://developers.pocketsmith.com
PocketSmith.NET Repository | https://github.com/LevelUpSoftware/PocketSmith.NET


## Getting Started
You'll need your PocketSmith UserId and an API Key.
*\*PocketSmith's Rest API support's OAuth2. OAuth2 is not currently supported in this library.*

## Services
This library uses a simple CRUD service architecture to make calls to PocketSmith's REST API.

- AccountService
- AttachmentService
- BudgetService
- CategoryService
- CategoryRulesService
- CurrencyService
- EventService
- InstitutionService
- LabelService
- SavedSearchService
- TimeZoneService
- TransactionAccountService
- TransactionService
- UserService

## Creating Service Instances
Services rely on HttpClient which requires careful management to avoid socket exhaustion.
There are three ways to create a service instance, two of which handle HttpClient instance management automatically.

### Create By Factory
Services can be instantiated using the PocketSmithServiceFactory.
Replace the userId and apiKey arguments with your [int]userId and [string]apiKey.
```
{
    var accountService = PocketSmithAccountFactory.CreateService<AccountService>(userId, "apiKey")
}
```

### Create By Dependency Injection - ASP.Net Core Only
Dependency injection uses the ASP.Net Core service provider to automatically manage dependencies.
This method requires the userId and apiKey to be stored in the application configuration. It uses MIcrosofts IConfiguration interface. You can store these parameters in appsettings.json, Azure configuration, etc.

Let's use the appsettings.json file for ASP.Net Core as an example. Here we've just added a section for the pocketsmith configs to the default settings file in a new ASP.Net Core project.

```
{
  "pocketSmith": {
    "userId": 12345,
    "apiKey": "insert_apiKey_here"
  }
}
```

Now we'll call the service collection extension to add PocketSmithServices to the service collection. This is normally done in the `Startup` or `Program` class.
*New projects built with .Net 6.0 & later do not have a Startup class by default and handle all the startup logic in the Program class.*

In the Program class:

```
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPocketSmithServices();

```

Finally, inject an instance of the required service using constructor injection.

```
public class SomeRandomService
{
    private readonly IAttachmentService _attachmentService;
    public SomeRandomService(IAttachmentService attachmentService)
    {
        _attachmentService = attachmentService;
    }

    public async Task DoSomethingAsync()
    {
        List<PocketSmithAttachment> attachments = await _attachmentService.GetAllAsync()
    }
}
```

### Create Directly (The Hard Way)
This is method requires extra care due to the fact that HttpClient instances are not managed automatically. 
You'll need to be sure to handle HttpClient management elsewhere in the application, otherwise you risk socket exhaustion which will cause your app to crash.

Here's an example of how you might do that.
For this example let's assume we're working with ASP.Net Core.

First, you'll want to make sure your startup sequence (either in the Startup or Program class) adds an HttpClient to the service collection.

```
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
```

Then, you can create instances of the service, along with the required `IApiHelper` dependency.
```
public class SomeRandomService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public SomRandomService(IHttpClientFactory httpClientFactory)
    {
      _httpClientFactory = httpClientFactory;
    }

    public async Task DoSomethingAsync()
    {
      var apiHelper = new ApiHelper(_httpClientFactory.CreateClient());

      //Replace '12345' and "apiKey" with the userId and apiKey.
      var transactionService = new TransactionService(apiHelper, 12345, "apiKey");

      var transactions = transactionService.GetAllAsync();
    }
}
```

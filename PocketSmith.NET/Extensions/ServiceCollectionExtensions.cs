using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PocketSmith.NET.Services.Accounts;
using PocketSmith.NET.Services.Attachments;
using PocketSmith.NET.Services.Budgets;
using PocketSmith.NET.Services.Categories;
using PocketSmith.NET.Services.CategoryRules;
using PocketSmith.NET.Services.Currencies;
using PocketSmith.NET.Services.Events;
using PocketSmith.NET.Services.Institutions;
using PocketSmith.NET.Services.Labels;
using PocketSmith.NET.Services.SavedSearches;
using PocketSmith.NET.Services.TimeZones;
using PocketSmith.NET.Services.TransactionAccounts;
using PocketSmith.NET.Services.Transactions;
using PocketSmith.NET.Services.Users;

namespace PocketSmith.NET.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPocketSmith(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var pocketSmithConfiguration = configuration.GetSection("pocketSmith").GetChildren();
        var userIdString = pocketSmithConfiguration.FirstOrDefault(x => x.Key == "userId")?.Value;
        var apiKey = pocketSmithConfiguration.FirstOrDefault(x => x.Key == "apiKey")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            throw new NullReferenceException($"Configuration value for 'userId' cannot be null or empty.");
        }

        if (string.IsNullOrEmpty(apiKey))
        {
            throw new NullReferenceException("Configuration value for 'apiKey' cannot be null or empty.");
        }

        var userId = int.Parse(userIdString);

        serviceCollection.AddScoped<IAccountService, AccountService>(s => new AccountService(userId, apiKey));
        serviceCollection.AddScoped<IAttachmentService, AttachmentService>(s => new AttachmentService(userId, apiKey));
        serviceCollection.AddScoped<IBudgetService, BudgetService>(s => new BudgetService(userId, apiKey));
        serviceCollection.AddScoped<ICategoryService, CategoryService>(s => new CategoryService(userId, apiKey));
        serviceCollection.AddScoped<ICategoryRuleService, CategoryRuleService>(s => new CategoryRuleService(userId, apiKey));
        serviceCollection.AddScoped<ICurrencyService, CurrencyService>(s => new CurrencyService(userId, apiKey));
        serviceCollection.AddScoped<IEventService, EventService>(s => new EventService(userId, apiKey));
        serviceCollection.AddScoped<IInstitutionService, InstitutionService>(s => new InstitutionService(userId, apiKey));
        serviceCollection.AddScoped<ILabelService, LabelService>(s => new LabelService(userId, apiKey));
        serviceCollection.AddScoped<ISavedSearchService, SavedSearchService>(s => new SavedSearchService(userId, apiKey));
        serviceCollection.AddScoped<ITimeZoneService, TimeZoneService>(s => new TimeZoneService(userId, apiKey));
        serviceCollection.AddScoped<ITransactionAccountService, TransactionAccountService>(s => new TransactionAccountService(userId, apiKey));
        serviceCollection.AddScoped<ITransactionService, TransactionService>(s => new TransactionService(userId, apiKey));
        serviceCollection.AddScoped<IUserService, UserService>(s => new UserService(userId, apiKey));
    }
}
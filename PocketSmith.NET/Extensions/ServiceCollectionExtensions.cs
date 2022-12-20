using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Services.Accounts;
using PocketSmith.NET.Services.Accounts.Validators;
using PocketSmith.NET.Services.Attachments;
using PocketSmith.NET.Services.Attachments.Validators;
using PocketSmith.NET.Services.Budgets;
using PocketSmith.NET.Services.Categories;
using PocketSmith.NET.Services.Categories.Validators;
using PocketSmith.NET.Services.CategoryRules;
using PocketSmith.NET.Services.Currencies;
using PocketSmith.NET.Services.Events;
using PocketSmith.NET.Services.Events.Validators;
using PocketSmith.NET.Services.Institutions;
using PocketSmith.NET.Services.Institutions.Validators;
using PocketSmith.NET.Services.Labels;
using PocketSmith.NET.Services.SavedSearches;
using PocketSmith.NET.Services.TimeZones;
using PocketSmith.NET.Services.TransactionAccounts;
using PocketSmith.NET.Services.Transactions;
using PocketSmith.NET.Services.Transactions.Validators;
using PocketSmith.NET.Services.Users;

namespace PocketSmith.NET.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPocketSmith(this IServiceCollection serviceCollection)
    {

        var serviceProvider = serviceCollection.BuildServiceProvider();

        serviceCollection.AddHttpClient();

        addValidators(serviceCollection);

        serviceCollection.AddScoped<IApiHelper, ApiHelper.ApiHelper>(s => new ApiHelper.ApiHelper(serviceProvider.GetService<HttpClient>()));

        serviceCollection.AddScoped<IAccountService, AccountService>(s => new AccountService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>(), serviceProvider.GetService<CreateAccountValidator>()));
        serviceCollection.AddScoped<IAttachmentService, AttachmentService>(s => new AttachmentService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>(), serviceProvider.GetService<CreateAttachmentValidator>()));
        serviceCollection.AddScoped<IBudgetService, BudgetService>(s => new BudgetService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>()));
        serviceCollection.AddScoped<ICategoryService, CategoryService>(s => new CategoryService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>(), serviceProvider.GetService<CreateCategoryValidator>()));
        serviceCollection.AddScoped<ICategoryRuleService, CategoryRuleService>(s => new CategoryRuleService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>()));
        serviceCollection.AddScoped<ICurrencyService, CurrencyService>(s => new CurrencyService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>()));
        serviceCollection.AddScoped<IEventService, EventService>(s => new EventService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>(), serviceProvider.GetService<CreateEventValidator>()));
        serviceCollection.AddScoped<IInstitutionService, InstitutionService>(s => new InstitutionService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>(), serviceProvider.GetService<CreateInstitutionValidator>()));
        serviceCollection.AddScoped<ILabelService, LabelService>(s => new LabelService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>()));
        serviceCollection.AddScoped<ISavedSearchService, SavedSearchService>(s => new SavedSearchService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>()));
        serviceCollection.AddScoped<ITimeZoneService, TimeZoneService>(s => new TimeZoneService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>()));
        serviceCollection.AddScoped<ITransactionAccountService, TransactionAccountService>(s => new TransactionAccountService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>()));
        serviceCollection.AddScoped<ITransactionService, TransactionService>(s => new TransactionService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>(), serviceProvider.GetService<CreateTransactionValidator>()));
        serviceCollection.AddScoped<IUserService, UserService>(s => new UserService(serviceProvider.GetService<IApiHelper>(), serviceProvider.GetService<IConfiguration>()));
    }

    private static void addValidators(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<CreateTransactionValidator>();
        serviceCollection.AddScoped<CreateInstitutionValidator>();
        serviceCollection.AddScoped<CreateEventValidator>();
        serviceCollection.AddScoped<CreateCategoryValidator>();
        serviceCollection.AddScoped<CreateAttachmentValidator>();
        serviceCollection.AddScoped<CreateAccountValidator>();
    }
}
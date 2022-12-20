using FluentValidation;
using Microsoft.Extensions.Configuration;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Institutions.Models;
using PocketSmith.NET.Services.Institutions.Validators;

namespace PocketSmith.NET.Services.Institutions;

public class InstitutionService : ServiceBase<PocketSmithInstitution, int>, IInstitutionService, IPocketSmithService
{
    private readonly CreateInstitutionValidator _createValidator;

    public InstitutionService(IApiHelper apiHelper, IConfiguration configuration,
        CreateInstitutionValidator createValidator) : base(apiHelper, configuration)
    {
        _createValidator = createValidator;
    }
    public InstitutionService(IApiHelper apiHelper, int userId, string apiKey, CreateInstitutionValidator createValidator) : base(apiHelper, userId, apiKey)
    {
        _createValidator = createValidator;
    }

    public new virtual async Task<IEnumerable<PocketSmithInstitution>> GetAllAsync()
    {
        return await base.GetAllAsync();
    }

    public virtual async Task<PocketSmithInstitution> CreateAsync(CreatePocketSmithInstitution createItem)
    {
        await _createValidator.ValidateAndThrowAsync(createItem);

        var uri = UriBuilder.AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithInstitution))
            .GetUriAndReset();

        var requestObject = new
        {
            title = createItem.Title,
            currency_code = createItem.CurrencyCode
        };

        var result = await ApiHelper.PostAsync<PocketSmithInstitution>(uri, requestObject);
        return result;
    }

    public virtual async Task DeleteAsync(int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithInstitution))
            .AddRoute(id.ToString())
            .GetUriAndReset();

        await ApiHelper.DeleteAsync(uri);
    }

    public new virtual async Task<PocketSmithInstitution> GetByIdAsync(int id)
    {
        return await base.GetByIdAsync(id);
    }

    public virtual async Task<PocketSmithInstitution> UpdateAsync(UpdatePocketSmithInstitution updatedInstitution, int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithInstitution))
            .AddRoute(id.ToString())
            .GetUriAndReset();

        var request = new
        {
            title = updatedInstitution.Title,
            currency_code = updatedInstitution.CurrencyCode
        };

        var results = await ApiHelper.PutAsync<PocketSmithInstitution>(uri, request);
        return results;
    }
}
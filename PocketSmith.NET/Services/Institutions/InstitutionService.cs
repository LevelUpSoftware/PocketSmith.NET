using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Institutions.Models;

namespace PocketSmith.NET.Services.Institutions;

public class InstitutionService : ServiceBase<PocketSmithInstitution, int>, IInstitutionService
{
    public InstitutionService(int userId, string apiKey) : base(userId, apiKey)
    {
    }

    public virtual async Task<PocketSmithInstitution> CreateAsync(CreatePocketSmithInstitution createItem)
    {
        var uri = UriBuilder.AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithInstitution))
            .Uri;

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
            .Uri;

        await ApiHelper.DeleteAsync(uri);
    }

    public virtual async Task<PocketSmithInstitution> UpdateAsync(UpdatePocketSmithInstitution updatedInstitution, int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithInstitution))
            .AddRoute(id.ToString())
            .Uri;

        var request = new
        {
            title = updatedInstitution.Title,
            currency_code = updatedInstitution.CurrencyCode
        };

        var results = await ApiHelper.PutAsync<PocketSmithInstitution>(uri, request);
        return results;
    }
}
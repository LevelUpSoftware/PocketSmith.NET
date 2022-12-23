using Microsoft.Extensions.Configuration;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.SavedSearches;

public class SavedSearchService : ServiceBase<PocketSmithSavedSearch, int>, ISavedSearchService, IPocketSmithService
{
    public SavedSearchService(IApiHelper apiHelper, IConfiguration configuration) : base(apiHelper, configuration)
    {
    }
    public SavedSearchService(IApiHelper apiHelper, int userId, string apiKey) : base(apiHelper, userId, apiKey)
    {
    }

    public new virtual async Task<IList<PocketSmithSavedSearch>> GetAllAsync()
    {
        return await base.GetAllAsync();
    }
}
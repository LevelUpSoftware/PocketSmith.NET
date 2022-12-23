using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.SavedSearches;

public interface ISavedSearchService
{
    Task<IList<PocketSmithSavedSearch>> GetAllAsync();
}
﻿using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.SavedSearches;

public class SavedSearchService : ServiceBase<PocketSmithSavedSearch, int>, ISavedSearchService
{
    public SavedSearchService(int userId, string apiKey) : base(userId, apiKey)
    {
    }

    public virtual async Task<IEnumerable<PocketSmithSavedSearch>> GetAllAsync()
    {
        return await base.GetAllAsync();
    }
}
﻿using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Accounts.Models;

namespace PocketSmith.NET.Services.Accounts;

public interface IAccountService
{
    Task<PocketSmithAccount?> GetByIdAsync(int id);
    Task<PocketSmithAccount> UpdateAsync(UpdatePocketSmithAccount updateAccount, int id);
    Task DeleteAsync(int id);
    Task<IEnumerable<PocketSmithAccount>> GetAllAsync();
    Task<IEnumerable<PocketSmithAccount>> UpdateDisplayOrder(List<int> accountIds);
    Task<PocketSmithAccount> CreateAsync(CreatePocketSmithAccount createItem);
    Task<IEnumerable<PocketSmithAccount>> GetAllByInstitutionIdAsync(int institutionId);
}
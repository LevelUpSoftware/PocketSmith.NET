﻿using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Categories.Models;
using PocketSmith.NET.Services.Categories.Options;

namespace PocketSmith.NET.Services.Categories;

public class CategoryService : ServiceBase<PocketSmithCategory, int>, ICategoryService
{
    public CategoryService(int userId, string apiKey) : base(userId, apiKey)
    {
    }
    public virtual async Task<PocketSmithCategory> CreateAsync(CreatePocketSmithCategory createItem)
    {
        var uri = UriBuilder.AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithCategory))
            .Uri;

        var request = new
        {
            title = createItem.Title,
            colour = createItem.Color,
            parent_id = createItem.ParentId,
            is_transfer = createItem.IsTransfer,
            is_bill = createItem.IsBill,
            roll_up = createItem.RollUp,
            refund_behaviour = createItem.RefundBehavior?.GetDisplayName()
        };

        var response = await ApiHelper.PostAsync<PocketSmithCategory>(uri, request);
        return response;
    }

    public virtual async Task DeleteAsync(int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithCategory))
            .AddRoute(id.ToString())
            .Uri;

        await ApiHelper.DeleteAsync(uri);
    }
    public virtual async Task<PocketSmithCategory> UpdateAsync(UpdatePocketSmithCategory updateItem, int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithCategory))
            .AddRoute(id.ToString())
            .Uri;

        var request = new
        {
            title = updateItem.Title,
            colour = updateItem.Color,
            parent_id = updateItem.ParentId,
            is_transfer = updateItem.IsTransfer,
            is_bill = updateItem.IsBill,
            roll_up = updateItem.RollUp,
            refund_behavior = updateItem.RefundBehavior
        };

        var response = await ApiHelper.PutAsync<PocketSmithCategory>(uri, request);
        return response;

    }


}
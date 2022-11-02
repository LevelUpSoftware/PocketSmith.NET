using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Attachments.Models;

namespace PocketSmith.NET.Services.Attachments;

public class AttachmentService : ServiceBase<PocketSmithAttachment, int>, IAttachmentService
{
    public AttachmentService(int userId, string apiKey) : base(userId, apiKey)
    {
    }
    public virtual async Task<PocketSmithAttachment> AssignToTransactionAsync(int transactionId, int attachmentId)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .AddRoute(transactionId.ToString())
            .AddRouteFromModel(typeof(PocketSmithAttachment))
            .Uri;

        var request = new
        {
            attachment_id = attachmentId
        };

        var response = await ApiHelper.PostAsync<PocketSmithAttachment>(uri, request);
        return response;
    }

    public virtual async Task<PocketSmithAttachment> CreateAsync(CreatePocketSmithAttachment createItem)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithAttachment))
            .Uri;

        var request = new
        {
            title = createItem.Title,
            file_name = createItem.FileName,
            file_data = createItem.FileData
        };

        var response = await ApiHelper.PostAsync<PocketSmithAttachment>(uri, request);
        return response;
    }

    public virtual async Task DeleteAsync(int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithAttachment))
            .AddRoute(id.ToString())
            .Uri;

        await ApiHelper.DeleteAsync(uri);
    }

    public virtual async Task<IEnumerable<PocketSmithAttachment>> GetAllAsync(bool onlyUnassigned = false)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithAttachment))
            .Uri;

        var response = await ApiHelper.GetAsync<List<PocketSmithAttachment>>(uri);
        return response;
    }

    public virtual async Task<IEnumerable<PocketSmithAttachment>> GetAllByTransactionIdAsync(int transactionId)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .AddRoute(transactionId.ToString())
            .AddRouteFromModel(typeof(PocketSmithAttachment))
            .Uri;

        var response = await ApiHelper.GetAsync<List<PocketSmithAttachment>>(uri);
        return response;
    }

    public virtual async Task UnAssignFromTransactionAsync(int transactionId, int attachmentId)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .AddRoute(transactionId.ToString())
            .AddRouteFromModel(typeof(PocketSmithAttachment))
            .AddRoute(attachmentId.ToString())
            .Uri;

        await ApiHelper.DeleteAsync(uri);
    }

    public virtual async Task<PocketSmithAttachment> UpdateAsync(PocketSmithAttachment updateItem, int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithAttachment))
            .AddRoute(id.ToString())
            .Uri;

        var request = new
        {
            title = updateItem.Title
        };

        var response = await ApiHelper.PutAsync<PocketSmithAttachment>(uri, updateItem);
        return response;
    }
}
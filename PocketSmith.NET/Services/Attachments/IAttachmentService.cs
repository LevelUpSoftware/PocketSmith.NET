using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Attachments.Models;

namespace PocketSmith.NET.Services.Attachments;

public interface IAttachmentService
{
    Task<PocketSmithAttachment?> GetByIdAsync(int id);
    Task<PocketSmithAttachment> UpdateAsync(string fileTitle, int id);
    Task DeleteAsync(int id);
    Task<IEnumerable<PocketSmithAttachment>> GetAllAsync(bool onlyUnassigned = false);
    Task<PocketSmithAttachment> CreateAsync(CreatePocketSmithAttachment createItem);
    Task<IEnumerable<PocketSmithAttachment>> GetAllByTransactionIdAsync(int transactionId);
    Task<PocketSmithAttachment> AssignToTransactionAsync(int transactionId, int attachmentId);
    Task UnAssignFromTransactionAsync(int transactionId, int attachmentId);
}
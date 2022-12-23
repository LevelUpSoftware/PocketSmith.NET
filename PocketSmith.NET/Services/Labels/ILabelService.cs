namespace PocketSmith.NET.Services.Labels;

public interface ILabelService
{
    Task<IList<string>> GetAllAsync();
}
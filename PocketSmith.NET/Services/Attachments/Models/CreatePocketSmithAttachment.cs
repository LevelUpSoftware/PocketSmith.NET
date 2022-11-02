namespace PocketSmith.NET.Services.Attachments.Models;

public class CreatePocketSmithAttachment
{
    public string Title { get; set; }
    public string FileName { get; set; }

    /// <summary>
    /// Base 64 encoded contents of the file. Supported file types are *.png, *.jpg, *.pdf, *.xls, *.xlsx, *.doc, *.docx.
    /// </summary>
    public string FileData { get; set; }
}
using FluentValidation;
using PocketSmith.NET.Services.Attachments.Models;

namespace PocketSmith.NET.Services.Attachments.Validators;

public class CreateAttachmentValidator : PocketSmithValidatorBase<CreatePocketSmithAttachment>
{
    public CreateAttachmentValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.FileData)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.FileName)
            .NotEmpty()
            .NotNull();
    }
}
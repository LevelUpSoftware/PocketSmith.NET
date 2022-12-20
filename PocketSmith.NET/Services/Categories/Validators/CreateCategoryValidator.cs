using FluentValidation;
using PocketSmith.NET.Services.Categories.Options;

namespace PocketSmith.NET.Services.Categories.Validators;

public class CreateCategoryValidator : PocketSmithValidatorBase<CreatePocketSmithCategory>
{
    public CreateCategoryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull();
    }
}
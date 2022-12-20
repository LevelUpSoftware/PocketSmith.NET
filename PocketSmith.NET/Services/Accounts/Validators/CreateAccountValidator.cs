using FluentValidation;
using PocketSmith.NET.Services.Accounts.Models;

namespace PocketSmith.NET.Services.Accounts.Validators;

public class CreateAccountValidator : PocketSmithValidatorBase<CreatePocketSmithAccount>
{
    public CreateAccountValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.InstitutionId)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.CurrencyCode)
            .NotNull()
            .NotEmpty();
    }
}
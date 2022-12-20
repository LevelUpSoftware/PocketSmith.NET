using FluentValidation;
using PocketSmith.NET.Services.Institutions.Models;

namespace PocketSmith.NET.Services.Institutions.Validators;

public class CreateInstitutionValidator : PocketSmithValidatorBase<CreatePocketSmithInstitution>
{
    public CreateInstitutionValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.CurrencyCode)
            .NotEmpty()
            .NotNull();
    }
}
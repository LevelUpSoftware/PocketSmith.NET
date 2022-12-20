using FluentValidation;
using PocketSmith.NET.Services.Events.Models;

namespace PocketSmith.NET.Services.Events.Validators;

public class CreateEventValidator : PocketSmithValidatorBase<CreatePocketSmithEvent>
{
    public CreateEventValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Amount)
            .NotEqual(0);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0);

        RuleFor(x => x.ScenarioId)
            .GreaterThan(0);
    }
}
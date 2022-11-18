using FluentValidation;
using FluentValidation.Results;
using PocketSmith.NET.Exceptions;

namespace PocketSmith.NET;

public abstract class PocketSmithValidatorBase<TModel> : AbstractValidator<TModel>
{
    protected override void RaiseValidationException(ValidationContext<TModel> context, ValidationResult result)
    {
        throw new PocketSmithValidationException(result.Errors.First().ErrorMessage);
    }
}
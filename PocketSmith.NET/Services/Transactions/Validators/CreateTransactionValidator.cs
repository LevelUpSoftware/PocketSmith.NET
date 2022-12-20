using FluentValidation;
using PocketSmith.NET.Services.Transactions.Models;

namespace PocketSmith.NET.Services.Transactions.Validators;

public class CreateTransactionValidator : PocketSmithValidatorBase<CreatePocketSmithTransaction>
{
    public CreateTransactionValidator()
    {
       ClassLevelCascadeMode = CascadeMode.Stop;

       RuleFor(x => x.Payee)
           .NotEmpty()
           .NotNull();

       RuleFor(x => x.TransactionAccountId)
           .NotEmpty()
           .NotNull()
           .GreaterThan(0);

       RuleFor(x => x.CategoryId)
           .GreaterThan(0);
    }
}
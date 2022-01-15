using FluentValidation;

namespace Application.CQRS.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("{FirstName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");
            RuleFor(p => p.LastName)
               .NotEmpty().WithMessage("{LastName} is required.")
               .NotNull();
            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("{DateOfBirth} is required.")
                .NotNull();
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{Email} is required.")
                .EmailAddress().WithMessage("{Email} is not valid.")
                .NotNull();
            RuleFor(p => p.PhoneNumber)
                .Matches(@"^((?:[0-9]\-?){6,14}[0-9])|((?:[0-9]\x20?){6,14}[0-9])$").WithMessage("PhoneNumber is not valid")
                .NotEmpty().WithMessage("{PhoneNumber} is required.")
                .NotNull();
            RuleFor(p => p.BankAccountNumber)
                 .NotEmpty().WithMessage("{BankAccountNumber} is required.")
                 .Matches(@"^\d$").WithMessage("Your Bank Account Number is not valid")
                 .NotNull();
        }


    }
}

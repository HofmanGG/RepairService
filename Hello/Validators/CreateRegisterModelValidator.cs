using FluentValidation;
using PL.Models;

namespace PL.Validators
{
    public class CreateRegisterModelValidator:AbstractValidator<RegisterModel>
    {
        public CreateRegisterModelValidator()
        {
            RuleFor(u => u.Email).EmailAddress();

            RuleFor(u => u.Password)
                .Equal(u => u.ConfirmPassword)
                .NotEmpty();

            RuleFor(u => u.FirstName)
                .MaximumLength(20)
                .NotEmpty();

            RuleFor(u => u.LastName)
                .MaximumLength(20)
                .NotEmpty();
        }
    }
}
using FluentValidation;
using PL.Models;

namespace PL.Validators
{
    public class CreateLoginModelValidator: AbstractValidator<LoginModel>
    {
        public CreateLoginModelValidator()
        {
            RuleFor(u => u.Email).EmailAddress();

            RuleFor(u => u.Password).NotEmpty();
        }
    }
}
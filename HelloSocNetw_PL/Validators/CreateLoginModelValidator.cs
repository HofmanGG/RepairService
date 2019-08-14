using FluentValidation;
using HelloSocNetw_PL.Models;

namespace HelloSocNetw_PL.Validators
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
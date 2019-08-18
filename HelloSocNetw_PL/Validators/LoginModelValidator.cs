using FluentValidation;
using HelloSocNetw_PL.Models;

namespace HelloSocNetw_PL.Validators
{
    public class LoginModelValidator: AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(u => u.Email).EmailAddress();

            RuleFor(u => u.Password).NotEmpty();
        }
    }
}
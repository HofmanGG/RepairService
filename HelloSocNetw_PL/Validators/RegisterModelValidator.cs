using System;
using FluentValidation;
using HelloSocNetw_PL.Models;

namespace HelloSocNetw_PL.Validators
{
    public class RegisterModelValidator:AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(u => u.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(u => u.Password)
                .Equal(u => u.ConfirmPassword);

            RuleFor(u => u.Password).NotNull();
            RuleFor(u => u.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters");
            RuleFor(u => u.Password).Matches(@"\d").WithMessage("Passwords must have at least 1 digit");
            RuleFor(u => u.Password).Matches(@"\W").WithMessage("Password must have at least 1 non alphanumeric");
            RuleFor(u => u.Password).Matches(u => @"[^" + u.Password[0] + "]+").WithMessage("Passwords must have unique char");

            RuleFor(u => u.FirstName)
                .NotNull()
                .Length(2, 20);

            RuleFor(u => u.LastName)
                .NotNull()
                .Length(2, 20);

            RuleFor(u => u.Gender)
                .NotNull();

            RuleFor(u => u.YearOfBirth)
                .NotNull()
                .GreaterThan(1900)
                .LessThan(DateTime.Now.Year - 5);

            RuleFor(u => u.MonthOfBirth)
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(12);

            RuleFor(u => u.DayOfBirth)
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(31);
        }
    }
}
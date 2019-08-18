using System;
using FluentValidation;
using HelloSocNetw_PL.Models;

namespace HelloSocNetw_PL.Validators
{
    public class RegisterModelValidator:AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
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

            RuleFor(u => u.Gender)
                .Matches("Male|Female");

            RuleFor(u => u.DayOfBirth)
                .GreaterThan(1900)
                .LessThan(DateTime.Now.Year - 5);

            RuleFor(u => u.MonthOfBirth)
                .GreaterThan(0)
                .LessThan(13);

            RuleFor(u => u.DayOfBirth)
                .GreaterThan(0)
                .LessThan(32);
        }
    }
}
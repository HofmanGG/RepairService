using FluentValidation;
using HelloSocNetw_PL.Models.UserInfoModels;
using System;

namespace HelloSocNetw_PL.Validators.UserInfoModelsValidators
{
    public class NewUserInfoModelValidator : AbstractValidator<NewUserInfoModel>
    {
        public NewUserInfoModelValidator()
        {
            RuleFor(u => u.FirstName)
                .NotNull()
                .Length(2, 20);

            RuleFor(u => u.LastName)
                .NotNull()
                .Length(2, 20);

            RuleFor(u => u.CountryId)
                .NotEmpty();

            RuleFor(u => u.Gender)
                .NotNull();

            RuleFor(u => u.YearOfBirth)
                .NotNull()
                .GreaterThan(1900)
                .LessThan(DateTime.Now.Year - 5);

            RuleFor(u => u.MonthOfBirth)
                .NotNull()
                .GreaterThan(0)
                .LessThan(13);

            RuleFor(u => u.DayOfBirth)
                .NotNull()
                .GreaterThan(0)
                .LessThan(32);
        }
    }
}

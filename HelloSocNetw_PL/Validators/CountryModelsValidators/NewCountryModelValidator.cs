using FluentValidation;
using HelloSocNetw_PL.Models.CountryModels;

namespace HelloSocNetw_PL.Validators.CountryModelsValidators
{
    public class NewCountryModelValidator : AbstractValidator<NewCountryModel>
    {
        public NewCountryModelValidator()
        {
            RuleFor(u => u.CountryName)
                .NotNull()
                .Length(2, 72);
            //.Matches(@"^[a-zA-Z]+$");
        }
    }
}

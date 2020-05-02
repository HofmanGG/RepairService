using FluentValidation;
using HelloSocNetw_PL.Models.CountryModels;

namespace HelloSocNetw_PL.Validators.CountryModelsValidators
{
    public class UpdateCountryModelValidator : AbstractValidator<UpdateCountryModel>
    {
        public UpdateCountryModelValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();

            RuleFor(c => c.CountryName)
                .NotNull()
                .Length(2, 30);
            //.Matches(@"^[a-zA-Z]+$");
        }
    }
}

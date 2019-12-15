using FluentValidation;
using HelloSocNetw_PL.Models.CountryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Validators.CountryModelsValidators
{
    public class UpdateCountryModelValidator : AbstractValidator<UpdateCountryModel>
    {
        public UpdateCountryModelValidator()
        {
            RuleFor(c => c.CountryId)
                .NotEmpty();

            RuleFor(c => c.CountryName)
                .NotNull()
                .Length(2, 30);
            //.Matches(@"^[a-zA-Z]+$");
        }
    }
}

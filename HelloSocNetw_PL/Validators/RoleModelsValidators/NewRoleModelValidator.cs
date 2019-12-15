using FluentValidation;
using HelloSocNetw_PL.Models.RoleModels;

namespace HelloSocNetw_PL.Validators.RoleModelsValidators
{
    public class NewRoleModelValidator : AbstractValidator<NewRoleModel>
    {
        public NewRoleModelValidator()
        {
            RuleFor(u => u.Name)
                .NotNull()
                .Length(2, 15);
        }
    }
}

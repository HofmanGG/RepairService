using FluentValidation;
using HelloSocNetw_PL.Models.RoleModels;

namespace HelloSocNetw_PL.Validators.RoleModelsValidators
{
    public class UpdateRoleModelValidator : AbstractValidator<UpdateRoleModel>
    {
        public UpdateRoleModelValidator()
        {
            RuleFor(u => u.Id)
                .NotEmpty();

            RuleFor(u => u.Name)
                .NotNull()
                .Length(2, 15);
        }
    }
}

using FluentValidation;
using HelloSocNetw_PL.Models.RepairRequestModels;

namespace HelloSocNetw_PL.Validators.RepairRequestModelsValidators
{
    public class NewRepairRequestModelValidator : AbstractValidator<NewRepairRequestModel>
    {
        public NewRepairRequestModelValidator()
        {
            RuleFor(rr => rr.Comment)
                .Length(4, 100);

            RuleFor(rr => rr.ProductName)
                .Length(4, 40);

            RuleFor(rr => rr.RepairStatus)
                .NotNull();
        }
    }
}

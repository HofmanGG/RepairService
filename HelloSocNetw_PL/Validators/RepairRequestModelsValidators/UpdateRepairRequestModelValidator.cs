using FluentValidation;
using HelloSocNetw_PL.Models.RepairRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Validators.RepairRequestModelsValidators
{
    public class UpdateRepairRequestModelValidator : AbstractValidator<UpdateRepairRequestModel>
    {
        public UpdateRepairRequestModelValidator()
        {
            RuleFor(rr => rr.RepairRequestId)
                .NotEmpty();

            RuleFor(rr => rr.Comment)
                .Length(4, 100);

            RuleFor(rr => rr.ProductName)
                .Length(4, 40);

            RuleFor(rr => rr.RepairStatus)
                .NotNull();
        }
    }
}

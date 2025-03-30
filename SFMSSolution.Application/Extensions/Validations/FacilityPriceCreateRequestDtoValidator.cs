using FluentValidation;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Extensions.Validations
{
    public class FacilityPriceCreateRequestDtoValidator : AbstractValidator<FacilityPriceCreateRequestDto>
    {
        public FacilityPriceCreateRequestDtoValidator()
        {
            RuleFor(x => x.FacilityTimeSlotId)
                .NotEmpty().WithMessage("Facility Time Slot Id is required.");

            RuleFor(x => x.Coefficient)
                .GreaterThan(0).WithMessage("Coefficient must be greater than zero.");

            RuleFor(x => x.FinalPrice)
                .GreaterThan(0).WithMessage("Final Price must be greater than zero.");
        }
    }
}

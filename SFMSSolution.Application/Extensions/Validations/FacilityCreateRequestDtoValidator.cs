using FluentValidation;
using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Extensions.Validations
{
    public class FacilityCreateRequestDtoValidator : AbstractValidator<FacilityCreateRequestDto>
    {
        public FacilityCreateRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Facility name is required.");
            RuleFor(x => x.FacilityType).NotEmpty().WithMessage("Facility type is required.");
            RuleFor(x => x.OwnerId).NotEmpty().WithMessage("OwnerId is required.");

        }
    }
}

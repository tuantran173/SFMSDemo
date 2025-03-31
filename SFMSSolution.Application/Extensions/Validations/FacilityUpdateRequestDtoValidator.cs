using FluentValidation;
using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Extensions.Validations
{
    public class FacilityUpdateRequestDtoValidator : AbstractValidator<FacilityUpdateRequestDto>
    {
        public FacilityUpdateRequestDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Facility ID is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Facility name is required.");
            RuleFor(x => x.FacilityType).NotEmpty().WithMessage("Facility type is required.");
        }
    }
}

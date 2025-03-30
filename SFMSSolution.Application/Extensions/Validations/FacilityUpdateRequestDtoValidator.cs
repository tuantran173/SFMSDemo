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
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");
        }
    }
}

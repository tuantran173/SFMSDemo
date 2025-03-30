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
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Location is required.");

            RuleFor(x => x.Capacity)
                .NotEmpty().WithMessage("Capacity is required.");
        }
    }
}

using FluentValidation;
using SFMSSolution.Application.DataTransferObjects.Event.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Extensions.Validations
{
    public class EventCreateRequestDtoValidator : AbstractValidator<EventCreateRequestDto>
    {
        public EventCreateRequestDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Event name is required.")
                .MaximumLength(100).WithMessage("Event name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime)
                .WithMessage("Start time must be before End time.");
        }
    }
}

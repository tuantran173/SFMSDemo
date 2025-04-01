using FluentValidation;
using SFMSSolution.Application.DataTransferObjects.Event.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Extensions.Validations
{
    public class EventUpdateRequestValidator : AbstractValidator<EventUpdateRequestDto>
    {
        public EventUpdateRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500).WithMessage("Image URL cannot exceed 500 characters.");

            RuleFor(x => x.EventType)
                .NotEmpty().WithMessage("Event type is required.")
                .MaximumLength(100).WithMessage("Event type cannot exceed 100 characters.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Start time is required.")
                .LessThan(x => x.EndTime).WithMessage("Start time must be before end time.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("End time is required.");

            RuleFor(x => x.Status)
                .Must(s => new[] { "Scheduled", "Ongoing", "Completed", "Cancelled" }.Contains(s))
                .WithMessage("Invalid status.");
        }
    }
}

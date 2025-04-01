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
            RuleFor(x => x.Title).NotEmpty().WithMessage("Event title is required.");
            RuleFor(x => x.StartTime).LessThan(x => x.EndTime).WithMessage("StartTime must be earlier than EndTime.");
            RuleFor(x => x.EventType).NotEmpty().WithMessage("Event type is required.");
        }
    }
}

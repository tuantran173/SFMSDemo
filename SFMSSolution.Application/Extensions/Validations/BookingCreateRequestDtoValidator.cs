using FluentValidation;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Extensions.Validations
{
    public class BookingCreateRequestDtoValidator : AbstractValidator<BookingCreateRequestDto>
    {
        public BookingCreateRequestDtoValidator()
        {
            RuleFor(x => x.BookingDate)
            .NotEmpty().WithMessage("Booking date is required.")
            .Must(date => date.Date >= DateTime.UtcNow.Date).WithMessage("Booking date cannot be in the past.");

            RuleFor(x => x.FacilityId)
                .NotEmpty().WithMessage("Facility is required.");

            RuleFor(x => x.FacilityTimeSlotId)
                .NotEmpty().WithMessage("Time slot is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User is required.");

            RuleFor(x => x.Note)
                .MaximumLength(1000).WithMessage("Note cannot exceed 1000 characters.");
        }
    }
}

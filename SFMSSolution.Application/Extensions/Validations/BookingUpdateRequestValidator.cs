using FluentValidation;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Extensions.Validations
{
    public class BookingUpdateRequestValidator : AbstractValidator<BookingUpdateRequestDto>
    {
        public BookingUpdateRequestValidator()
        {
            RuleFor(x => x.BookingDate)
                .NotEmpty().WithMessage("Booking date is required.");

            RuleFor(x => x.FacilityTimeSlotId)
                .NotEmpty().WithMessage("Time slot is required.");

            RuleFor(x => x.Note)
                .MaximumLength(1000).WithMessage("Note is too long.");

            RuleFor(x => x.Status)
                .Must(status =>
                    string.IsNullOrEmpty(status) ||
                    new[] { "Pending", "Confirmed", "Canceled" }.Contains(status))
                .WithMessage("Invalid status.");
        }
    }
}

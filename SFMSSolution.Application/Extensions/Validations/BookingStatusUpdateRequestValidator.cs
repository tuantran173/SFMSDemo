using FluentValidation;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Extensions.Validations
{
    public class BookingStatusUpdateRequestValidator : AbstractValidator<BookingStatusUpdateRequestDto>
    {
        public BookingStatusUpdateRequestValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(s => new[] { "Confirmed", "Canceled" }.Contains(s))
                .WithMessage("Status must be either Confirmed or Canceled.");
        }
    }
}

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
            RuleFor(x => x.FacilityId)
                .NotEmpty().WithMessage("Facility Id is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User Id is required.");

            RuleFor(x => x.BookingDate)
                .NotNull().WithMessage("Booking date is required.");
        }
    }
}

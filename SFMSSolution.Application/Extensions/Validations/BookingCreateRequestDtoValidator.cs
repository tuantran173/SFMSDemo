using FluentValidation;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using System;

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

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Start time is required.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("End time is required.")
                .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(200).WithMessage("Customer name cannot exceed 200 characters.");

            RuleFor(x => x.CustomerPhone)
                .NotEmpty().WithMessage("Customer phone is required.")
                .Matches(@"^\d{9,11}$").WithMessage("Customer phone must be 9–11 digits.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required.")
                .Must(method => method =="VNPay")
                .WithMessage("Payment method must be 'Tiền mặt' or 'VNPay'.");

            RuleFor(x => x.FinalPrice)
                .GreaterThan(0).WithMessage("Final price must be greater than zero.");

            RuleFor(x => x.Note)
                .MaximumLength(1000).WithMessage("Note cannot exceed 1000 characters.");
        }
    }
}

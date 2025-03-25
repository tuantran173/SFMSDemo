using FluentValidation;
using SFMSSolution.Application.DataTransferObjects.Auth.Request;
using SFMSSolution.Application.DataTransferObjects.Booking.Request;
using SFMSSolution.Application.DataTransferObjects.Facility.Request;
using SFMSSolution.Application.DataTransferObjects.FacilityPrice.Request;
using SFMSSolution.Application.DataTransferObjects.User.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Extensions.Validations
{
    public class AuthRequestDtoValidator : AbstractValidator<AuthRequestDto>
    {
        public AuthRequestDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }
    }

    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match.");
            RuleFor(x => x.FullName).NotEmpty();
        }
    }

    public class FacilityCreateRequestDtoValidator : AbstractValidator<FacilityCreateRequestDto>
    {
        public FacilityCreateRequestDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Capacity).NotEmpty().GreaterThan("0");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category is required.");
        }
    }

    public class BookingCreateRequestDtoValidator : AbstractValidator<BookingCreateRequestDto>
    {
        public BookingCreateRequestDtoValidator()
        {
            RuleFor(x => x.FacilityId).NotEmpty().WithMessage("Facility is required.");
            RuleFor(x => x.StartTime).LessThan(x => x.EndTime).WithMessage("Start time must be before End time.");
        }
    }

    public class ChangePasswordRequestDtoValidator : AbstractValidator<ChangePasswordRequestDto>
    {
        public ChangePasswordRequestDtoValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty();
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6);
            RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessage("Passwords do not match.");
        }
    }

    public class FacilityPriceCreateRequestDtoValidator : AbstractValidator<FacilityPriceCreateRequestDto>
    {
        public FacilityPriceCreateRequestDtoValidator()
        {
            RuleFor(x => x.FacilityTimeSlotId).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Coefficient).GreaterThan(0).WithMessage("Coefficient must be greater than zero.");
            RuleFor(x => x.StartTime).LessThan(x => x.EndTime).WithMessage("StartTime must be before EndTime.");
        }
    }
}

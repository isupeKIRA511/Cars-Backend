using FluentValidation;
using RentARide.Application.DTOs.Rentals;
using System;

namespace RentARide.Application.Rentals.Commands.CreateRental
{
    public class CreateRentalValidator : AbstractValidator<CreateRentalRequest>
    {
        public CreateRentalValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.VehicleId)
                .NotEmpty().WithMessage("Vehicle ID is required.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Start date cannot be in the past.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after the start date.");
        }
    }
}

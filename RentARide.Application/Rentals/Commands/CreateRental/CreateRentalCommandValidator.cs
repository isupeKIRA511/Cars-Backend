using FluentValidation;

namespace RentARide.Application.Rentals.Commands.CreateRental
{
    public class CreateRentalCommandValidator : AbstractValidator<CreateRentalCommand>
    {
        public CreateRentalCommandValidator()
        {
            RuleFor(v => v.Request.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(v => v.Request.VehicleId)
                .NotEmpty().WithMessage("Vehicle ID is required.");

            RuleFor(v => v.Request.StartDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage("Start date cannot be in the past.");

            RuleFor(v => v.Request.EndDate)
                .GreaterThan(v => v.Request.StartDate)
                .WithMessage("End date must be after start date.");
        }
    }
}

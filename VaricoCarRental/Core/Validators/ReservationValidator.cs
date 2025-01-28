using FluentValidation;
using VaricoCarRental.Models;

namespace VaricoCarRental.Validators;

public class ReservationValidator : AbstractValidator<Reservation>
{
    public ReservationValidator()
    {
        RuleFor(r => r.VehicleId)
            .NotEmpty().WithMessage("VehicleId is required.");

        RuleFor(r => r.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(r => r.StartDate)
            .NotEmpty().WithMessage("StartDate is required.")
            .LessThan(r => r.EndDate).WithMessage("StartDate must be before EndDate.");

        RuleFor(r => r.EndDate)
            .NotEmpty().WithMessage("EndDate is required.");
    }
}

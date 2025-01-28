using FluentValidation;
using VaricoCarRental.Models;

namespace VaricoCarRental.Validators;


public class VehicleValidator : AbstractValidator<Vehicle>
{
    public VehicleValidator()
    {
        RuleFor(v => v.Make)
            .NotEmpty().WithMessage("Make is required.");

        RuleFor(v => v.Model)
            .NotEmpty().WithMessage("Model is required.");

        RuleFor(v => v.Year)
            .GreaterThan(0).WithMessage("Year must be greater than 0.")
            .LessThanOrEqualTo(DateTime.Now.Year + 1).WithMessage("Year cannot be in the future.");

        RuleFor(v => v.LicensePlate)
            .NotEmpty().WithMessage("License plate is required.");
    }
}
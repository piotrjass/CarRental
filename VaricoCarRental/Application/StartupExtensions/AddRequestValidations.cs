using FluentValidation.AspNetCore;
using VaricoCarRental.Validators;

namespace VaricoCarRental.StartupExtensions

    {
    public static class ValidationExtensions
    {
        public static IServiceCollection AddRequestValidations(this IServiceCollection services)
        {
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ReservationValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<VehicleValidator>());

            return services;
        }
    }
    }

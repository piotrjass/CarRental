using VaricoCarRental.Enums;

namespace VaricoCarRental.Models;

public class Vehicle
{
    public Guid Id { get; set; }
    public string Make { get; set; } // Brand of the vehicle, e.g., Toyota
    public string Model { get; set; } // Model of the vehicle, e.g., Corolla
    public int Year { get; set; } // Year of manufacture
    public string LicensePlate { get; set; } // Vehicle's license plate number
    public VehicleStatus Status { get; set; } // Availability status (e.g., Available, Reserved, InMaintenance)
}
using VaricoCarRental.Enums;

namespace VaricoCarRental.Models;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; } // Reference to the reserved vehicle
    public Guid UserId { get; set; } // Reference to the user making the reservation
    public DateTime StartDate { get; set; } // Start date of the reservation
    public DateTime EndDate { get; set; } // End date of the reservation
    public ReservationStatus Status { get; set; } // Status of the reservation
    public DateTime? ReturnDate { get; set; } // Date when the vehicle was returned (if applicable)
}
using MediatR;

namespace VaricoCarRental.Commands;

public class UpdateVehicleCommand : IRequest
{
    public Guid VehicleId { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public int Year { get; set; }
    public Enums.VehicleStatus Status { get; set; }
}
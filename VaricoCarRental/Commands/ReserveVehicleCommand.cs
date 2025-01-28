using MediatR;

namespace VaricoCarRental.Commands;

public class ReserveVehicleCommand : IRequest
{
    public Guid VehicleId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
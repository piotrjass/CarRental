using MediatR;

namespace VaricoCarRental.Commands;

public class UpdateReservationCommand : IRequest
{
    public Guid ReservationId { get; set; }
    public Guid VehicleId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Enums.ReservationStatus Status { get; set; }
}
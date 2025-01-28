using MediatR;

namespace VaricoCarRental.Commands;

public class ReturnVehicleCommand : IRequest
{
    public Guid ReservationId { get; set; }
    public DateTime ReturnDate { get; set; }
}
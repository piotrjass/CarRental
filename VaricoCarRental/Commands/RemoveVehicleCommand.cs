using MediatR;

namespace VaricoCarRental.Commands;

public class RemoveVehicleCommand : IRequest<bool>
{
    public Guid VehicleId { get; }

    public RemoveVehicleCommand(Guid vehicleId)
    {
        VehicleId = vehicleId;
    }
}
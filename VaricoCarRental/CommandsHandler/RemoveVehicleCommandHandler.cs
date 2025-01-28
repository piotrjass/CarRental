using MediatR;
using VaricoCarRental.Commands;
using VaricoCarRental.IRepositories;

namespace VaricoCarRental.CommandsHandler;

public class RemoveVehicleCommandHandler : IRequestHandler<RemoveVehicleCommand, bool>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveVehicleCommandHandler(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
    {
        _vehicleRepository = vehicleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RemoveVehicleCommand request, CancellationToken cancellationToken)
    {
        // Find the vehicle in the database
        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
        if (vehicle == null)
        {
            return false; // Vehicle not found
        }

        // Remove the vehicle
        _vehicleRepository.Remove(vehicle);

        // Save changes
        await _unitOfWork.SaveChangesAsync();

        return true; // Successfully removed
    }
}
using MediatR;
using VaricoCarRental.Commands;
using VaricoCarRental.IRepositories;

namespace VaricoCarRental.CommandsHandler;

public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVehicleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateVehicleCommand command, CancellationToken cancellationToken)
    {
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(command.VehicleId);
        if (vehicle == null)
        {
            throw new InvalidOperationException("Vehicle not found.");
        }

        // Update vehicle properties
        vehicle.Model = command.Model;
        vehicle.LicensePlate = command.LicensePlate;
        vehicle.Year = command.Year;
        vehicle.Status = command.Status;

        await _unitOfWork.VehicleRepository.UpdateAsync(vehicle);
        await _unitOfWork.SaveChangesAsync();
        
    }
}
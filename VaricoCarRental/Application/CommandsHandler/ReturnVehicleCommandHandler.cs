using MediatR;
using VaricoCarRental.Commands;
using VaricoCarRental.Enums;
using VaricoCarRental.IRepositories;

namespace VaricoCarRental.CommandsHandler;

public class ReturnVehicleCommandHandler : IRequestHandler<ReturnVehicleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReturnVehicleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ReturnVehicleCommand command, CancellationToken cancellationToken)
    {
        var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(command.ReservationId);
        if (reservation == null || reservation.Status != ReservationStatus.Active)
        {
            throw new InvalidOperationException("Invalid or inactive reservation.");
        }

        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(reservation.VehicleId);
        if (vehicle == null)
        {
            throw new InvalidOperationException("Vehicle associated with the reservation not found.");
        }

        // Update reservation status to Completed
        reservation.Status = ReservationStatus.Completed;
        reservation.ReturnDate = command.ReturnDate;

        // Update vehicle status to Available
        vehicle.Status = VehicleStatus.Available;

        await _unitOfWork.ReservationRepository.UpdateAsync(reservation);
        await _unitOfWork.VehicleRepository.UpdateAsync(vehicle);
        await _unitOfWork.SaveChangesAsync();
    }
}

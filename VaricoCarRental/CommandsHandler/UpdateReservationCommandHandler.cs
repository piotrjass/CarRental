using MediatR;
using VaricoCarRental.Commands;
using VaricoCarRental.IRepositories;

namespace VaricoCarRental.CommandsHandler;

public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateReservationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateReservationCommand command, CancellationToken cancellationToken)
    {
        var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(command.ReservationId);
        if (reservation == null)
        {
            throw new InvalidOperationException("Reservation not found.");
        }

        // Update reservation properties
        reservation.VehicleId = command.VehicleId;
        reservation.UserId = command.UserId;
        reservation.StartDate = command.StartDate;
        reservation.EndDate = command.EndDate;
        reservation.Status = command.Status;

        await _unitOfWork.ReservationRepository.UpdateAsync(reservation);
        await _unitOfWork.SaveChangesAsync();
        
    }
}
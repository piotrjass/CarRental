using MediatR;
using VaricoCarRental.Commands;
using VaricoCarRental.Enums;
using VaricoCarRental.IRepositories;
using VaricoCarRental.Models;

namespace VaricoCarRental.CommandsHandler;

public class ReserveVehicleCommandHandler : IRequestHandler<ReserveVehicleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReserveVehicleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ReserveVehicleCommand command, CancellationToken cancellationToken)
    {
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(command.VehicleId);
        if (vehicle == null || vehicle.Status != VehicleStatus.Available)
        {
            throw new InvalidOperationException("Vehicle is not available.");
        }

        // Sprawdź, czy pojazd jest już zarezerwowany w podanym okresie
        var existingReservations = await _unitOfWork.ReservationRepository.GetAllReservations();
        var isVehicleReserved = existingReservations.Any(r =>
            r.VehicleId == command.VehicleId &&
            r.Status == ReservationStatus.Active &&  // Sprawdzenie, czy rezerwacja jest aktywna
            r.StartDate < command.EndDate &&         // Sprawdzenie, czy daty rezerwacji się nakładają
            r.EndDate > command.StartDate
        );

        if (isVehicleReserved)
        {
            throw new InvalidOperationException("The vehicle is already reserved for the selected period.");
        }
    
        vehicle.Status = VehicleStatus.Reserved;
        await _unitOfWork.VehicleRepository.UpdateAsync(vehicle);

        // Utwórz rezerwację
        var reservation = new Reservation
        {
            Id = Guid.NewGuid(),
            VehicleId = command.VehicleId,
            UserId = command.UserId,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            Status = ReservationStatus.Active
        };

        // Zapisz rezerwację w repozytorium
        await _unitOfWork.ReservationRepository.AddAsync(reservation);
        await _unitOfWork.SaveChangesAsync();
    }
}

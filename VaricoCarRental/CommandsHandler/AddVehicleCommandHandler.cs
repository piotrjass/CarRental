using MediatR;
using VaricoCarRental.Commands;
using VaricoCarRental.Enums;
using VaricoCarRental.IRepositories;
using VaricoCarRental.Models;

namespace VaricoCarRental.CommandsHandler;

public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddVehicleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
    {
        // Walidacja i tworzenie nowego pojazdu
        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Make = request.Make,
            Model = request.Model,
            Year = request.Year,
            LicensePlate = request.LicensePlate,
            Status = VehicleStatus.Available
        };

        // Dodanie pojazdu do repozytorium
        await _unitOfWork.VehicleRepository.AddAsync(vehicle);
        await _unitOfWork.SaveChangesAsync(); // Zatwierdzenie zmian w bazie danych

        return vehicle.Id; // Zwrócenie ID nowo dodanego pojazdu
    }
}
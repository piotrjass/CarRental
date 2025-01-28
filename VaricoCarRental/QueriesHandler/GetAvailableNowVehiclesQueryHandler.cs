using MediatR;
using VaricoCarRental.Enums;
using VaricoCarRental.IRepositories;
using VaricoCarRental.Models;
using VaricoCarRental.Queries;

namespace VaricoCarRental.QueriesHandler;

public class GetAvailableNowVehiclesQueryHandler : IRequestHandler<GetAvailableNowVehiclesQuery, List<Vehicle>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAvailableNowVehiclesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Vehicle>> Handle(GetAvailableNowVehiclesQuery query, CancellationToken cancellationToken)
    {
        // Pobranie wszystkich pojazdów, które są dostępne
        var vehicles = await _unitOfWork.VehicleRepository.GetAllAsync();
            
        // Filtrujemy pojazdy, które są dostępne
        var availableVehicles = vehicles
            .Where(v => v.Status == VehicleStatus.Available)
            .ToList();
            
        return availableVehicles;
    }
}
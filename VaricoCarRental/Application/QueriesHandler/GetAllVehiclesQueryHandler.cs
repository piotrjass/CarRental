using MediatR;
using VaricoCarRental.IRepositories;
using VaricoCarRental.Models;
using VaricoCarRental.Queries;

namespace VaricoCarRental.QueriesHandler;

public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, List<Vehicle>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllVehiclesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Vehicle>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        // Pobranie wszystkich pojazdów z repozytorium
        var vehicles = await _unitOfWork.VehicleRepository.GetAllVehiclesAsync();
        return vehicles;
    }
}
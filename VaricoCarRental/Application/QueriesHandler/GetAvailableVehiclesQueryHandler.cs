using MediatR;
using VaricoCarRental.IRepositories;
using VaricoCarRental.Models;
using VaricoCarRental.Queries;

namespace VaricoCarRental.QueriesHandler;

public class GetAvailableVehiclesQueryHandler : IRequestHandler<GetAvailableVehiclesQuery, List<Vehicle>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAvailableVehiclesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Vehicle>> Handle(GetAvailableVehiclesQuery query, CancellationToken cancellationToken)
    {
        // Pobierz dostępne pojazdy w zadanym okresie
        return await _unitOfWork.VehicleRepository.GetAvailableVehicles(query.StartDate, query.EndDate);
    }
}
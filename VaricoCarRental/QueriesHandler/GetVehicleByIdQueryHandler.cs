using MediatR;
using VaricoCarRental.IRepositories;
using VaricoCarRental.Models;
using VaricoCarRental.Queries;

namespace VaricoCarRental.QueriesHandler;

public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, Vehicle>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetVehicleByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Vehicle> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        // Pobranie pojazdu z repozytorium po ID
        return await _unitOfWork.VehicleRepository.GetByIdAsync(request.VehicleId);
    }
}
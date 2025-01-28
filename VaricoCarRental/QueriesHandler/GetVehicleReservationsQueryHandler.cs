using MediatR;
using VaricoCarRental.IRepositories;
using VaricoCarRental.Models;
using VaricoCarRental.Queries;

namespace VaricoCarRental.QueriesHandler;

public class GetVehicleReservationsQueryHandler : IRequestHandler<GetVehicleReservationsQuery, List<Reservation>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetVehicleReservationsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Reservation>> Handle(GetVehicleReservationsQuery query, CancellationToken cancellationToken)
    {
        // Pobranie rezerwacji dla pojazdu w określonym przedziale czasowym
        return await _unitOfWork.ReservationRepository.GetAllReservations();
    }
}
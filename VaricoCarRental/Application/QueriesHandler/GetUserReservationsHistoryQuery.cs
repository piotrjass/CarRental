using MediatR;
using VaricoCarRental.IRepositories;
using VaricoCarRental.Models;
using VaricoCarRental.Queries;

namespace VaricoCarRental.QueriesHandler;

public class GetUserReservationsHistoryQueryHandler : IRequestHandler<GetUserReservationsHistoryQuery, List<Reservation>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserReservationsHistoryQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Reservation>> Handle(GetUserReservationsHistoryQuery query, CancellationToken cancellationToken)
    {
        // Pobranie historii rezerwacji użytkownika
        var reservations = await _unitOfWork.ReservationRepository.GetReservationsByUserId(query.UserId);

        return reservations;
    }
}
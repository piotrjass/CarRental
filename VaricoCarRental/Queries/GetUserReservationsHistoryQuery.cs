using MediatR;
using VaricoCarRental.Models;

namespace VaricoCarRental.Queries;

public class GetUserReservationsHistoryQuery : IRequest<List<Reservation>>
{
    public Guid UserId { get; set; }
}
using MediatR;
using VaricoCarRental.Models;

namespace VaricoCarRental.Queries;

public class GetAvailableNowVehiclesQuery : IRequest<List<Vehicle>>
{
}
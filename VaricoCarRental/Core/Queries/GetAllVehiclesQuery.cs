using MediatR;
using VaricoCarRental.Models;

namespace VaricoCarRental.Queries;

public class GetAllVehiclesQuery : IRequest<List<Vehicle>>
{
}
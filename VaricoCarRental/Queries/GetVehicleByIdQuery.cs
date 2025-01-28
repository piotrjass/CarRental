using MediatR;
using VaricoCarRental.Models;

namespace VaricoCarRental.Queries;

public class GetVehicleByIdQuery : IRequest<Vehicle>
{
    public Guid VehicleId { get; set; }
}
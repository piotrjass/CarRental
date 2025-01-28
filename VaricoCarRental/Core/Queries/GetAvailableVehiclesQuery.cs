using MediatR;
using VaricoCarRental.Models;

namespace VaricoCarRental.Queries;

public class GetAvailableVehiclesQuery :  IRequest<List<Vehicle>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
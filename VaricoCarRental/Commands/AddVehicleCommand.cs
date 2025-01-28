using MediatR;

namespace VaricoCarRental.Commands;

public class AddVehicleCommand : IRequest<Guid>
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string LicensePlate { get; set; }
}
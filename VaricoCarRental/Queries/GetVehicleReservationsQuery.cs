using MediatR;
using VaricoCarRental.Models;

namespace VaricoCarRental.Queries;


    public class GetVehicleReservationsQuery : IRequest<List<Reservation>>
    {
        public Guid VehicleId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

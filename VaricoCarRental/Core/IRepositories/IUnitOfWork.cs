namespace VaricoCarRental.IRepositories;

public interface IUnitOfWork
{
    IReservationRepository ReservationRepository { get; }
    IVehicleRepository VehicleRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
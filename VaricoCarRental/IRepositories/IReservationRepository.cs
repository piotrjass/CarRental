using VaricoCarRental.Models;

namespace VaricoCarRental.IRepositories;

public interface IReservationRepository
{
    Task<List<Reservation>> GetReservationsByUserId(Guid userId);
    Task<List<Reservation>> GetReservations(Guid vehicleId, DateTime? fromDate, DateTime? toDate);
    Task<List<Reservation>> GetAllReservations();
    Task<Reservation> GetByIdAsync(Guid id);
    Task AddAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
}
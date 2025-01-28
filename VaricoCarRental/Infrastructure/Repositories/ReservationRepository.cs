using Microsoft.EntityFrameworkCore;
using VaricoCarRental.IRepositories;
using VaricoCarRental.Models;

namespace VaricoCarRental.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ReservationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Reservation>> GetReservations(Guid vehicleId, DateTime? fromDate, DateTime? toDate)
    {
        var query = _dbContext.Set<Reservation>().AsQueryable();

        if (fromDate.HasValue)
        {
            query = query.Where(r => r.EndDate >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(r => r.StartDate <= toDate.Value);
        }

        return await query.Where(r => r.VehicleId == vehicleId).ToListAsync();
    }
    
    public async Task<List<Reservation>> GetAllReservations()
    {
        // Pobranie wszystkich rezerwacji bez żadnego filtra
        return await _dbContext.Set<Reservation>().ToListAsync();
    }

    public async Task<Reservation> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<Reservation>().FindAsync(id);
    }

    public async Task AddAsync(Reservation reservation)
    {
        await _dbContext.Set<Reservation>().AddAsync(reservation);
    }

    public async Task UpdateAsync(Reservation reservation)
    {
        _dbContext.Set<Reservation>().Update(reservation);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<List<Reservation>> GetReservationsByUserId(Guid userId)
    {
        return await _dbContext.Reservations
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }
}
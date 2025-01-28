using Microsoft.EntityFrameworkCore;
using VaricoCarRental.Enums;
using VaricoCarRental.IRepositories;
using VaricoCarRental.Models;

namespace VaricoCarRental.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VehicleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Pobranie dostępnych pojazdów na podstawie daty
    public async Task<List<Vehicle>> GetAvailableVehicles(DateTime startDate, DateTime endDate)
    {
        return await _dbContext.Set<Vehicle>()
            .Where(v => v.Status == VehicleStatus.Available &&
                        !_dbContext.Set<Reservation>().Any(r => r.VehicleId == v.Id &&
                                                                r.StartDate < endDate &&
                                                                r.EndDate > startDate))
            .ToListAsync();
    }

    // Pobranie pojazdu na podstawie jego ID
    public async Task<Vehicle> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<Vehicle>().FindAsync(id);
    }

    // Dodanie nowego pojazdu do repozytorium
    public async Task AddAsync(Vehicle vehicle)
    {
        await _dbContext.Set<Vehicle>().AddAsync(vehicle);
    }

    // Zaktualizowanie pojazdu w repozytorium
    public async Task UpdateAsync(Vehicle vehicle)
    {
        _dbContext.Set<Vehicle>().Update(vehicle);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Vehicle>> GetAllVehiclesAsync()
    {
        return await _dbContext.Set<Vehicle>().ToListAsync();
    }
    
    public async Task<List<Vehicle>> GetAllAsync()
    {
        return await _dbContext.Vehicles.ToListAsync();
    }
    
    public void Remove(Vehicle vehicle)
    {
        _dbContext.Vehicles.Remove(vehicle);
    }
}

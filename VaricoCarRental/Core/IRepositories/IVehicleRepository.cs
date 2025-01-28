using VaricoCarRental.Models;

namespace VaricoCarRental.IRepositories;

public interface IVehicleRepository
{
    Task<List<Vehicle>> GetAvailableVehicles(DateTime startDate, DateTime endDate);
    Task<Vehicle> GetByIdAsync(Guid id);
    Task AddAsync(Vehicle vehicle);
    Task UpdateAsync(Vehicle vehicle);
    
    Task<List<Vehicle>> GetAllVehiclesAsync();
    
    Task<List<Vehicle>> GetAllAsync();
    
    void Remove(Vehicle vehicle);
    
    
    
}

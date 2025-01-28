using VaricoCarRental.IRepositories;

namespace VaricoCarRental.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IReservationRepository _reservationRepository;
    private IVehicleRepository _vehicleRepository;
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IReservationRepository ReservationRepository => _reservationRepository ??= new ReservationRepository(_context);
    public IVehicleRepository VehicleRepository => _vehicleRepository ??= new VehicleRepository(_context);
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
    
    
}
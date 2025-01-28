using VaricoCarRental.Models;

namespace VaricoCarRental;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext: IdentityDbContext<User> {
    
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().Property(u => u.Initials).HasMaxLength(5);
        builder.HasDefaultSchema("identity");
    }
}
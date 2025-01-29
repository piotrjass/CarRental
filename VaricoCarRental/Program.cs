using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VaricoCarRental;
using VaricoCarRental.IRepositories;
using VaricoCarRental.Middlewares;
using VaricoCarRental.Models;
using VaricoCarRental.Repositories;
using VaricoCarRental.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

// Rejestracja DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Rejestracja UnitOfWork i Repozytoriów
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Dodanie MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Dodanie kontrolerów i Swaggera
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Identity
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:4200")  // Adres front-endu Angular
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}
app.UseCors("AllowLocalhost");
app.MapGet("users/me", async (ClaimsPrincipal claims, ApplicationDbContext context) =>
    {
        string userId = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return await context.Users.FindAsync(userId);
    })
    .RequireAuthorization();

app.UseHttpsRedirection();

// app.UseAuthorization();
app.MapControllers();
app.MapIdentityApi<User>();

app.Run();
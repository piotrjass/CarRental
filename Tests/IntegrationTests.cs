using AutoFixture;
using Microsoft.EntityFrameworkCore;
using VaricoCarRental;
using VaricoCarRental.Enums;
using VaricoCarRental.IRepositories;
using VaricoCarRental.Models;
using VaricoCarRental.Repositories;

namespace Tests;

public class IntegrationTests
{
    private readonly IFixture _fixture;
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=Varico;";

    public IntegrationTests()
    {
        _fixture = new Fixture();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_connectionString)
            .Options;

        var dbContext = new ApplicationDbContext(options);
        _unitOfWork = new UnitOfWork(dbContext);
    }

    [Fact]
    public async Task Should_AddVehicleToDatabase()
    {
        // Arrange
        var vehicle = _fixture.Create<Vehicle>(); // Generate a vehicle using AutoFixture
        vehicle.Make = "Toyota"; // Example make
        vehicle.Model = "Corolla"; // Example model
        vehicle.Year = 2021; // Example year
        vehicle.LicensePlate = "ABC-123"; // Example license plate
        vehicle.Status = VehicleStatus.Available; // Example status

        // Act
        await _unitOfWork.VehicleRepository.AddAsync(vehicle);
        await _unitOfWork.SaveChangesAsync();

        // Assert - Check if the vehicle is added to the database
        var savedVehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(vehicle.Id);

        Assert.NotNull(savedVehicle); // Ensure the vehicle is found
        Assert.Equal(vehicle.Make, savedVehicle.Make); // Assert that the make matches
        Assert.Equal(vehicle.Model, savedVehicle.Model); // Assert that the model matches
        Assert.Equal(vehicle.Year, savedVehicle.Year); // Assert that the year matches
        Assert.Equal(vehicle.LicensePlate, savedVehicle.LicensePlate); // Assert that the license plate matches
        Assert.Equal(vehicle.Status, savedVehicle.Status); // Assert that the status matches
    }

    [Fact]
    public async Task Should_UpdateVehicleStatus_Successfully()
    {
        // Arrange
        var vehicle = _fixture.Create<Vehicle>(); // Generate a vehicle using AutoFixture
        vehicle.Make = "Toyota"; // Example make
        vehicle.Model = "Corolla"; // Example model
        vehicle.Year = 2021; // Example year
        vehicle.LicensePlate = "ABC-123"; // Example license plate
        vehicle.Status = VehicleStatus.Available; // Initial status as Available

        // Add the vehicle to the database
        await _unitOfWork.VehicleRepository.AddAsync(vehicle);
        await _unitOfWork.SaveChangesAsync();

        // Act
        // Change the vehicle status to 'Reserved'
        vehicle.Status = VehicleStatus.Reserved; // New status
        await _unitOfWork.VehicleRepository.UpdateAsync(vehicle); // Update the vehicle in the repository
        await _unitOfWork.SaveChangesAsync();

        // Assert - Verify if the vehicle's status has been updated
        var updatedVehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(vehicle.Id);

        Assert.NotNull(updatedVehicle); // Ensure the vehicle is found
        Assert.Equal(VehicleStatus.Reserved, updatedVehicle.Status); // Assert that the status was updated to 'Reserved'
    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaricoCarRental.Commands;
using VaricoCarRental.Queries;

namespace VaricoCarRental.Controllers;


    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Dodanie nowego pojazdu
        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] AddVehicleCommand command)
        {
            if (command == null)
            {
                return BadRequest(new { Message = "Invalid vehicle data." });
            }

            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetVehicleById), new { vehicleId = result }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Pobranie szczegółów pojazdu
        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetVehicleById([FromRoute] Guid vehicleId)
        {
            try
            {
                var vehicle = await _mediator.Send(new GetVehicleByIdQuery { VehicleId = vehicleId });
                if (vehicle == null)
                {
                    return NotFound(new { Message = "Vehicle not found" });
                }

                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableVehicles()
        {
            var query = new GetAvailableNowVehiclesQuery();
            var availableVehicles = await _mediator.Send(query);

            if (availableVehicles == null || availableVehicles.Count == 0)
            {
                return NotFound(new { Message = "No available vehicles found." });
            }

            return Ok(availableVehicles);
        }
        
        // Pobranie dostępnych pojazdów w określonym okresie
        [HttpGet("availablity-check")]
        public async Task<IActionResult> GetAvailableVehicles([FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var vehicles = await _mediator.Send(new GetAvailableVehiclesQuery
                    { StartDate = startDate, EndDate = endDate });
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Pobranie wszystkich pojazdów
        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            try
            {
                var vehicles = await _mediator.Send(new GetAllVehiclesQuery());
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        
        /// <summary>
        /// Endpoint to delete a vehicle by its ID
        /// </summary>
        /// <param name="id">The ID of the vehicle to delete</param>
        /// <returns>ActionResult</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            var result = await _mediator.Send(new RemoveVehicleCommand(id));

            if (result)
            {
                return NoContent(); // HTTP 204 if successfully deleted
            }

            return NotFound(new { Message = "Vehicle not found" }); // HTTP 404 if vehicle not found
        }
    }
        
    

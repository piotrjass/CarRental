using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaricoCarRental.Commands;
using VaricoCarRental.Queries;

namespace VaricoCarRental.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehicleReservationController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehicleReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("reserve")]
    public async Task<IActionResult> ReserveVehicle([FromBody] ReserveVehicleCommand command)
    {
        try
        {
            // Wysłanie komendy do MediatR
            await _mediator.Send(command);
            return Ok(new { Message = "Vehicle reserved successfully!" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("return")]
    public async Task<IActionResult> ReturnVehicle([FromBody] ReturnVehicleCommand command)
    {
        try
        {
            // Wysłanie komendy do MediatR
            await _mediator.Send(command);
            return Ok(new { Message = "Vehicle returned successfully!" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    
    [HttpGet("reservations")]
    public async Task<IActionResult> GetAllReservations()
    {
        try
        {
            // Wysłanie zapytania do MediatR
            var reservations = await _mediator.Send(new GetVehicleReservationsQuery());
            return Ok(reservations);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
    
    [HttpGet("history/{userId}")]
    public async Task<IActionResult> GetUserReservationsHistory(Guid userId)
    {
        var query = new GetUserReservationsHistoryQuery { UserId = userId };
        var reservations = await _mediator.Send(query);

        if (reservations == null || reservations.Count == 0)
        {
            return NotFound(new { Message = "No reservations found for this user." });
        }

        return Ok(reservations);
    }
}
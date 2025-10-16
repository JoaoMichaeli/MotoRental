using Microsoft.AspNetCore.Mvc;
using MotoRental.Api.Application.DTOs;
using MotoRental.Api.Application.Services;
using MotoRental.Api.Exceptions;

namespace MotoRental.Api.Controllers;

[ApiController]
[Route("api/rentals")]
public class RentalsController : ControllerBase
{
    private readonly IRentalService _service;
    public RentalsController(IRentalService service) { _service = service; }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRentalRequest request)
    {
        try
        {
            var dto = await _service.CreateRentalAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
        catch (BusinessException ex) { return BadRequest(new { message = ex.Message }); }
    }

    [HttpPatch("{id:guid}/return")]
    public async Task<IActionResult> Return(Guid id, [FromQuery] DateTime actualReturnDate)
    {
        try
        {
            var dto = await _service.ReturnRentalAsync(id, actualReturnDate);
            return Ok(dto);
        }
        catch (BusinessException ex) { return BadRequest(new { message = ex.Message }); }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }
}

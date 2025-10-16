using Microsoft.AspNetCore.Mvc;
using MotoRental.Api.Application.DTOs;
using MotoRental.Api.Application.Services;
using MotoRental.Api.Exceptions;

namespace MotoRental.Api.Controllers;

[ApiController]
[Route("api/motorcycles")]
public class MotorcyclesController : ControllerBase
{
    private readonly IMotorcycleService _service;
    public MotorcyclesController(IMotorcycleService service) { _service = service; }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMotorcycleRequest request)
    {
        try
        {
            var dto = await _service.CreateMotorcycleAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? plate)
    {
        var list = await _service.GetMotorcyclesAsync(plate);
        return Ok(list);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromServices] IMotorcycleService svc, Guid id)
    {
        var all = await svc.GetMotorcyclesAsync(null);
        var item = all.FirstOrDefault(m => m.Id == id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPatch("{id:guid}/plate")]
    public async Task<IActionResult> UpdatePlate(Guid id, [FromBody] string newPlate)
    {
        try
        {
            await _service.UpdatePlateAsync(id, newPlate);
            return NoContent();
        }
        catch (BusinessException ex) { return BadRequest(new { message = ex.Message }); }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (BusinessException ex) { return BadRequest(new { message = ex.Message }); }
    }
}

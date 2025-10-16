using Microsoft.AspNetCore.Mvc;
using MotoRental.Api.Application.DTOs;
using MotoRental.Api.Application.Services;
using MotoRental.Api.Exceptions;

namespace MotoRental.Api.Controllers;

[ApiController]
[Route("api/riders")]
public class RidersController : ControllerBase
{
    private readonly IRiderService _service;
    public RidersController(IRiderService service) { _service = service; }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRiderRequest request)
    {
        try
        {
            var dto = await _service.CreateRiderAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
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

using Microsoft.AspNetCore.Mvc;
using MotoRental.Api.Application.DTOs;
using MotoRental.Api.Application.Services;
using MotoRental.Api.Exceptions;

namespace MotoRental.Api.Controllers;

[ApiController]
[Route("entregadores")]
public class RidersController : ControllerBase
{
    private readonly IRiderService _service;
    public RidersController(IRiderService service) { _service = service; }

    /// <summary>
    /// Cadastrar entregador
    /// </summary>
    /// <param name="request">Dados do entregador a ser cadastrado.</param>
    /// <response code="400">Dados inválidos</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRiderRequest request)
    {
        try
        {
            var dto = await _service.CreateRiderAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Enviar foto da CNH
    /// </summary>
    /// <response code="400">Dados inválidos</response>
    [HttpPost("{id:guid}/cnh")]
    public async Task<IActionResult> UploadCnh(Guid id, IFormFile cnh)
    {
        if (cnh == null || cnh.Length == 0)
            return BadRequest(new { message = "Invalid CNH file." });

        return Ok(new { message = $"CNH uploaded successfully for rider {id}." });
    }

    /// <summary>
    /// Consultar entregadores existentes
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }
}

using Microsoft.AspNetCore.Mvc;
using MotoRental.Api.Application.DTOs;
using MotoRental.Api.Application.Services;
using MotoRental.Api.Exceptions;

namespace MotoRental.Api.Controllers;

[ApiController]
[Route("locacao")]
public class RentalsController : ControllerBase
{
    private readonly IRentalService _service;
    public RentalsController(IRentalService service) { _service = service; }

    /// <summary>
    /// Alugar uma moto
    /// </summary>
    /// <param name="request">Dados do entregador a ser cadastrado.</param>
    /// <response code="400">Dados inválidos</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRentalRequest request)
    {
        try
        {
            var dto = await _service.CreateRentalAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Consultar locação por id
    /// </summary>
    /// <response code="400">Dados inválidos</response>
    /// <response code="404">Dados não encontrados</response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var dto = await _service.GetByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// Informar data de devolução e calcular valor
    /// </summary>
    /// <response code="200">Data de devolução informada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPut("{id:guid}/devolucao")]
    public async Task<IActionResult> Devolucao(Guid id, [FromQuery] DateTime dataDevolucao)
    {
        try
        {
            var dto = await _service.ReturnRentalAsync(id, dataDevolucao);
            return Ok(dto);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

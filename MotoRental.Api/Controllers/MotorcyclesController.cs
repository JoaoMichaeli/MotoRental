using Microsoft.AspNetCore.Mvc;
using MotoRental.Api.Exceptions;
using MotoRental.Application.Interfaces;
using MotoRental.Common.DTOs.Motorcycle.Request;

namespace MotoRental.Api.Controllers;

[ApiController]
[Route("motos")]
public class MotorcyclesController : ControllerBase
{
    private readonly IMotorcycleService _service;
    public MotorcyclesController(IMotorcycleService service) { _service = service; }

    /// <summary>
    /// Cadastra uma nova moto
    /// </summary>
    /// <param name="request">Dados da moto a ser cadastrada.</param>
    /// <returns>Retorna a moto criada</returns>
    /// <response code="400">Dados inválidos</response>
    /// <response code="500">Ocorreu um erro interno no servidor</response>
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
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    /// <summary>
    /// Consultar motos existentes
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? plate)
    {
        var list = await _service.GetMotorcyclesAsync(plate);
        return Ok(list);
    }

    /// <summary>
    /// Modificar a placa de uma moto
    /// </summary>
    /// <response code="200">Placa modificada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPut("{id:guid}/placa")]
    public async Task<IActionResult> UpdatePlate(Guid id, [FromBody] string newPlate)
    {
        try
        {
            await _service.UpdatePlateAsync(id, newPlate);
            return NoContent();
        }
        catch (BusinessException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Consultar motos existentes por id
    /// </summary>
    /// <response code="200">Detalhes da moto</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="404">Moto não encotrada</response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var list = await _service.GetMotorcyclesAsync(null);
        var item = list.FirstOrDefault(m => m.Id == id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// Remover uma moto
    /// </summary>
    /// <response code="400">Dados inválidos</response>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (BusinessException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

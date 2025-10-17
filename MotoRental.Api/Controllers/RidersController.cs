using Microsoft.AspNetCore.Mvc;
using MotoRental.Api.Exceptions;
using MotoRental.Application.Interfaces;
using MotoRental.Common.DTOs.Rider.Request;

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
            return BadRequest(new { message = "Arquivo CNH inválido." });

        var allowedExtensions = new[] { ".png", ".bmp" };
        var ext = Path.GetExtension(cnh.FileName)?.ToLower();
        if (!allowedExtensions.Contains(ext))
            return BadRequest(new { message = "Formato inválido. Apenas PNG ou BMP são aceitos." });

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadsCNH");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{id}{ext}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await cnh.CopyToAsync(stream);

        return Ok(new { message = $"CNH enviada com sucesso para o entregador {id}.", path = filePath });
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

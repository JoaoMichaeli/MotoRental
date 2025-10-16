using MotoRental.Api.Application.DTOs;

namespace MotoRental.Api.Application.Services;

public interface IMotorcycleService
{
    Task<MotorcycleDto> CreateMotorcycleAsync(CreateMotorcycleRequest request);
    Task<IEnumerable<MotorcycleDto>> GetMotorcyclesAsync(string? plateFilter = null);
    Task UpdatePlateAsync(Guid id, string newPlate);
    Task DeleteAsync(Guid id);
}

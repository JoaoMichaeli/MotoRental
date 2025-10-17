using MotoRental.Common.DTOs.Motorcycle.Request;
using MotoRental.Common.DTOs.Motorcycle.Response;

namespace MotoRental.Application.Interfaces;

public interface IMotorcycleService
{
    Task<MotorcycleResponseDto> CreateMotorcycleAsync(CreateMotorcycleRequest request);
    Task<IEnumerable<MotorcycleResponseDto>> GetMotorcyclesAsync(string? plateFilter = null);
    Task UpdatePlateAsync(Guid id, string newPlate);
    Task DeleteAsync(Guid id);
}

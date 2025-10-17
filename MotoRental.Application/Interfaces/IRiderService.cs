using MotoRental.Common.DTOs.Rider.Request;
using MotoRental.Common.DTOs.Rider.Response;

namespace MotoRental.Application.Interfaces;

public interface IRiderService
{
    Task<RiderResponseDto> CreateRiderAsync(CreateRiderRequest request);
    Task<RiderResponseDto?> GetByIdAsync(Guid id);
}

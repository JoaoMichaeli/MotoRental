using MotoRental.Api.Application.DTOs;

namespace MotoRental.Api.Application.Services;

public interface IRiderService
{
    Task<RiderDto> CreateRiderAsync(CreateRiderRequest request);
    Task<RiderDto?> GetByIdAsync(Guid id);
}

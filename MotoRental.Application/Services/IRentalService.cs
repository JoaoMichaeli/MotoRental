using MotoRental.Api.Application.DTOs;

namespace MotoRental.Api.Application.Services;

public interface IRentalService
{
    Task<RentalResponseDto> CreateRentalAsync(CreateRentalRequest request);
    Task<RentalResponseDto> ReturnRentalAsync(Guid rentalId, DateTime actualReturnDate);
    Task<RentalResponseDto?> GetByIdAsync(Guid id);
}

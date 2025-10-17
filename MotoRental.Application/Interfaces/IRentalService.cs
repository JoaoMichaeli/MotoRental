using MotoRental.Common.DTOs.Rental.Request;
using MotoRental.Common.DTOs.Rental.Response;

namespace MotoRental.Application.Interfaces;

public interface IRentalService
{
    Task<RentalResponseDto> CreateRentalAsync(CreateRentalRequest request);
    Task<RentalResponseDto> ReturnRentalAsync(Guid rentalId, DateTime actualReturnDate);
    Task<RentalResponseDto?> GetByIdAsync(Guid id);
}

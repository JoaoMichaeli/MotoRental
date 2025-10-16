using MotoRental.Api.Enums;

namespace MotoRental.Api.Application.DTOs;

public class RentalResponseDto
{
    public Guid Id { get; set; }
    public Guid MotorcycleId { get; set; }
    public Guid RiderId { get; set; }
    public RentalPlan Plan { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public decimal PricePerDay { get; set; }
}

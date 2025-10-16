using MotoRental.Api.Enums;

namespace MotoRental.Api.Application.DTOs;

public class CreateRentalRequest
{
    public Guid MotorcycleId { get; set; }
    public Guid RiderId { get; set; }
    public RentalPlan Plan { get; set; }
    public DateTime ExpectedEndDate { get; set; }
}

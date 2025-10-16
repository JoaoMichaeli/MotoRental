using MotoRental.Api.Enums;

namespace MotoRental.Api.Domain.Entities;

public class Rental
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid MotorcycleId { get; set; }
    public Motorcycle? Motorcycle { get; set; }

    public Guid RiderId { get; set; }
    public Rider? Rider { get; set; }

    public RentalPlan Plan { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime StartDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }

    public DateTime? ActualEndDate { get; set; }

    public decimal PricePerDay { get; set; }
}

namespace MotoRental.Api.Domain.Entities;

public class Motorcycle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Year { get; set; }
    public string Model { get; set; } = default!;
    public string Plate { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

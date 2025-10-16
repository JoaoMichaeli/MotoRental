namespace MotoRental.Api.Application.DTOs;

public class MotorcycleDto
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public string Model { get; set; } = default!;
    public string Plate { get; set; } = default!;
}

namespace MotoRental.Api.Application.DTOs;

public class CreateMotorcycleRequest
{
    public int Year { get; set; }
    public string Model { get; set; } = default!;
    public string Plate { get; set; } = default!;
}

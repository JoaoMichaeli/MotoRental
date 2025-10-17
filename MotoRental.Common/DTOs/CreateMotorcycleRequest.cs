using System.Text.Json.Serialization;

namespace MotoRental.Api.Application.DTOs;

public class CreateMotorcycleRequest
{
    [JsonPropertyName("ano")]
    public int Year { get; set; }

    [JsonPropertyName("modelo")]
    public string Model { get; set; } = default!;

    [JsonPropertyName("placa")]
    public string Plate { get; set; } = default!;
}

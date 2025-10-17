using System.Text.Json.Serialization;

namespace MotoRental.Api.Application.DTOs;

    public class MotorcycleDto
    {
        [JsonPropertyName("identificador")]
        public Guid Id { get; set; }

        [JsonPropertyName("ano")]
        public int Year { get; set; }

        [JsonPropertyName("modelo")]
        public string Model { get; set; } = default!;

        [JsonPropertyName("placa")]
        public string Plate { get; set; } = default!;
    }

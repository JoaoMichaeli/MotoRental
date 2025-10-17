using System.Text.Json.Serialization;

namespace MotoRental.Common.DTOs.Motorcycle.Response;

    public class MotorcycleResponseDto
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

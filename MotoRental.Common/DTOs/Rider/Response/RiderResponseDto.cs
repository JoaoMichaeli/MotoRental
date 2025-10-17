using MotoRental.Api.Enums;

namespace MotoRental.Common.DTOs.Rider.Response;

public class RiderResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Cnpj { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string CnhNumber { get; set; } = default!;
    public LicenseType LicenseType { get; set; }
}

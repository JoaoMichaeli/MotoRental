using MotoRental.Api.Enums;

namespace MotoRental.Api.Application.DTOs;

public class CreateRiderRequest
{
    public string Name { get; set; } = default!;
    public string Cnpj { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string CnhNumber { get; set; } = default!;
    public LicenseType LicenseType { get; set; }
}

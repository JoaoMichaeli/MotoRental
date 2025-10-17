using MotoRental.Api.Enums;

namespace MotoRental.Api.Domain.Entities;

public class Rider
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string Cnpj { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string CnhNumber { get; set; } = default!;
    public LicenseType LicenseType { get; set; }
}

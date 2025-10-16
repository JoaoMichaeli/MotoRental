using MotoRental.Api.Enums;

namespace MotoRental.Api.Domain.Entities;

public class Rider
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string Cnpj { get; set; } = default!; // unico
    public DateTime BirthDate { get; set; }
    public string CnhNumber { get; set; } = default!; // unico
    public LicenseType LicenseType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

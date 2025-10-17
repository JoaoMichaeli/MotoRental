using MotoRental.Api.Domain.Entities;

namespace MotoRental.Infrastructure.Interfaces;

public interface IMotorcycleRepository
{
    Task AddAsync(Motorcycle moto);
    Task<Motorcycle?> GetByIdAsync(Guid id);
    Task<Motorcycle?> GetByPlateAsync(string plate);
    Task<IEnumerable<Motorcycle>> GetAllAsync(string? plateFilter = null);
    Task UpdateAsync(Motorcycle moto);
    Task DeleteAsync(Motorcycle moto);
    Task<bool> HasRentalsAsync(Guid motorcycleId);
}

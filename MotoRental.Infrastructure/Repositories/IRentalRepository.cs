using MotoRental.Api.Domain.Entities;

namespace MotoRental.Api.Infrastructure.Repositories;

public interface IRentalRepository
{
    Task AddAsync(Rental rental);
    Task<Rental?> GetByIdAsync(Guid id);
    Task<IEnumerable<Rental>> GetByMotorcycleIdAsync(Guid motorcycleId);
    Task UpdateAsync(Rental rental);
}

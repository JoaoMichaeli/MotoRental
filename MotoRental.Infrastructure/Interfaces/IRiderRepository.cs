using MotoRental.Api.Domain.Entities;

namespace MotoRental.Infrastructure.Interfaces;

public interface IRiderRepository
{
    Task AddAsync(Rider rider);
    Task<Rider?> GetByIdAsync(Guid id);
    Task<Rider?> GetByCnpjAsync(string cnpj);
    Task<Rider?> GetByCnhNumberAsync(string cnhNumber);
}

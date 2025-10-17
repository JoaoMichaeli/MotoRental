using Microsoft.EntityFrameworkCore;
using MotoRental.Api.Domain.Entities;
using MotoRental.Api.Infrastructure.Persistence;

namespace MotoRental.Api.Infrastructure.Repositories;

public class RiderRepository : IRiderRepository
{
    private readonly MotoRentalDbContext _db;

    public async Task AddAsync(Rider rider)
    {
        _db.Riders.Add(rider);
        await _db.SaveChangesAsync();
    }

    public async Task<Rider?> GetByIdAsync(Guid id) => await _db.Riders.FindAsync(id);

    public async Task<Rider?> GetByCnpjAsync(string cnpj) =>
        await _db.Riders.FirstOrDefaultAsync(r => r.Cnpj == cnpj);

    public async Task<Rider?> GetByCnhNumberAsync(string cnhNumber) =>
        await _db.Riders.FirstOrDefaultAsync(r => r.CnhNumber == cnhNumber);
}

using Microsoft.EntityFrameworkCore;
using MotoRental.Api.Domain.Entities;
using MotoRental.Api.Infrastructure.Persistence;
using MotoRental.Infrastructure.Interfaces;

namespace MotoRental.Api.Infrastructure.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly MotoRentalDbContext _db;

    public async Task AddAsync(Rental rental)
    {
        _db.Rentals.Add(rental);
        await _db.SaveChangesAsync();
    }

    public async Task<Rental?> GetByIdAsync(Guid id) => await _db.Rentals
        .Include(r => r.Motorcycle)
        .Include(r => r.Rider)
        .FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IEnumerable<Rental>> GetByMotorcycleIdAsync(Guid motorcycleId) =>
        await _db.Rentals.Where(r => r.MotorcycleId == motorcycleId).ToListAsync();

    public async Task UpdateAsync(Rental rental)
    {
        _db.Rentals.Update(rental);
        await _db.SaveChangesAsync();
    }
}

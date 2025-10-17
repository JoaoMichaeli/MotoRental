using Microsoft.EntityFrameworkCore;
using MotoRental.Api.Domain.Entities;
using MotoRental.Api.Infrastructure.Persistence;

namespace MotoRental.Api.Infrastructure.Repositories;

public class MotorcycleRepository : IMotorcycleRepository
{
    private readonly MotoRentalDbContext _db;

    public async Task AddAsync(Motorcycle moto)
    {
        _db.Motorcycles.Add(moto);
        await _db.SaveChangesAsync();
    }

    public async Task<Motorcycle?> GetByIdAsync(Guid id) =>
        await _db.Motorcycles.FindAsync(id);

    public async Task<Motorcycle?> GetByPlateAsync(string plate) =>
        await _db.Motorcycles.FirstOrDefaultAsync(m => m.Plate == plate);

    public async Task<IEnumerable<Motorcycle>> GetAllAsync(string? plateFilter = null)
    {
        var q = _db.Motorcycles.AsQueryable();
        if (!string.IsNullOrWhiteSpace(plateFilter))
            q = q.Where(m => m.Plate.Contains(plateFilter));
        return await q.ToListAsync();
    }

    public async Task UpdateAsync(Motorcycle moto)
    {
        _db.Motorcycles.Update(moto);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Motorcycle moto)
    {
        _db.Motorcycles.Remove(moto);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> HasRentalsAsync(Guid motorcycleId)
    {
        return await _db.Rentals.AnyAsync(r => r.MotorcycleId == motorcycleId);
    }
}

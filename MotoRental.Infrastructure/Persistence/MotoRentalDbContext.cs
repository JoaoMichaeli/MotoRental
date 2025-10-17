using Microsoft.EntityFrameworkCore;
using MotoRental.Api.Domain.Entities;

namespace MotoRental.Api.Infrastructure.Persistence;

public class MotoRentalDbContext : DbContext
{
    public DbSet<Motorcycle> Motorcycles => Set<Motorcycle>();
    public DbSet<Rider> Riders => Set<Rider>();
    public DbSet<Rental> Rentals => Set<Rental>();
}

using Microsoft.EntityFrameworkCore;
using MotoRental.Api.Domain.Entities;

namespace MotoRental.Api.Infrastructure.Persistence;

public class MotoRentalDbContext : DbContext
{
    public MotoRentalDbContext(DbContextOptions<MotoRentalDbContext> options) : base(options) { }

    public DbSet<Motorcycle> Motorcycles => Set<Motorcycle>();
    public DbSet<Rider> Riders => Set<Rider>();
    public DbSet<Rental> Rentals => Set<Rental>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Motorcycle>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Plate).IsRequired();
            b.HasIndex(x => x.Plate).IsUnique();
        });

        modelBuilder.Entity<Rider>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Cnpj).IsRequired();
            b.HasIndex(x => x.Cnpj).IsUnique();

            b.Property(x => x.CnhNumber).IsRequired();
            b.HasIndex(x => x.CnhNumber).IsUnique();
        });

        modelBuilder.Entity<Rental>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasOne(x => x.Motorcycle).WithMany().HasForeignKey(x => x.MotorcycleId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Rider).WithMany().HasForeignKey(x => x.RiderId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}

using Xunit;
using FluentAssertions;
using MotoRental.Api.Domain.Entities;
using MotoRental.Api.Enums;
using MotoRental.Common.Mapping;

namespace MotoRental.Tests
{
    public class MappingExtensionsTests
    {
        [Fact]
        public void Motorcycle_ToDto_ShouldMapAllProperties()
        {
            var moto = new Motorcycle
            {
                Id = Guid.NewGuid(),
                Year = 2020,
                Model = "Fan",
                Plate = "RKLO823"
            };

            var dto = moto.ToDto();

            dto.Should().NotBeNull();
            dto.Id.Should().Be(moto.Id);
            dto.Year.Should().Be(moto.Year);
            dto.Model.Should().Be(moto.Model);
            dto.Plate.Should().Be(moto.Plate);
        }

        [Fact]
        public void Rider_ToDto_ShouldMapAllProperties()
        {
            var rider = new Rider
            {
                Id = Guid.NewGuid(),
                Name = "Joao",
                Cnpj = "123456789",
                BirthDate = new DateTime(2000, 1, 1),
                CnhNumber = "ABC1234",
                LicenseType = LicenseType.A
            };

            var dto = rider.ToDto();

            dto.Should().NotBeNull();
            dto.Id.Should().Be(rider.Id);
            dto.Name.Should().Be(rider.Name);
            dto.Cnpj.Should().Be(rider.Cnpj);
            dto.BirthDate.Should().Be(rider.BirthDate);
            dto.CnhNumber.Should().Be(rider.CnhNumber);
            dto.LicenseType.Should().Be(rider.LicenseType);
        }

        [Fact]
        public void Rental_ToDto_ShouldMapAllProperties()
        {
            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                MotorcycleId = Guid.NewGuid(),
                RiderId = Guid.NewGuid(),
                Plan = RentalPlan.Days7,
                StartDate = DateTime.Today,
                ExpectedEndDate = DateTime.Today.AddDays(6),
                ActualEndDate = null,
                PricePerDay = 30m
            };

            var dto = rental.ToDto();

            dto.Should().NotBeNull();
            dto.Id.Should().Be(rental.Id);
            dto.MotorcycleId.Should().Be(rental.MotorcycleId);
            dto.RiderId.Should().Be(rental.RiderId);
            dto.Plan.Should().Be(rental.Plan);
            dto.StartDate.Should().Be(rental.StartDate);
            dto.ExpectedEndDate.Should().Be(rental.ExpectedEndDate);
            dto.ActualEndDate.Should().Be(rental.ActualEndDate);
            dto.PricePerDay.Should().Be(rental.PricePerDay);
        }
    }
}

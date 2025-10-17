using Moq;
using FluentAssertions;
using MotoRental.Api.Application.Services;
using MotoRental.Api.Application.DTOs;
using MotoRental.Api.Infrastructure.Repositories;
using MotoRental.Api.Domain.Entities;
using MotoRental.Api.Enums;
using MotoRental.Api.Exceptions;

namespace MotoRental.Tests.Services
{
    public class RentalServiceTests
    {
        private readonly Mock<IRentalRepository> _rentalRepoMock;
        private readonly Mock<IMotorcycleRepository> _motoRepoMock;
        private readonly Mock<IRiderRepository> _riderRepoMock;
        private readonly RentalService _service;

        public RentalServiceTests()
        {
            _rentalRepoMock = new Mock<IRentalRepository>();
            _motoRepoMock = new Mock<IMotorcycleRepository>();
            _riderRepoMock = new Mock<IRiderRepository>();
            _service = new RentalService(_rentalRepoMock.Object, _motoRepoMock.Object, _riderRepoMock.Object);
        }

        [Fact]
        public async Task CreateRentalAsync_ShouldCreate_WhenValid()
        {
            var motoId = Guid.NewGuid();
            var riderId = Guid.NewGuid();

            _motoRepoMock.Setup(r => r.GetByIdAsync(motoId)).ReturnsAsync(new Motorcycle { Id = motoId });
            _riderRepoMock.Setup(r => r.GetByIdAsync(riderId)).ReturnsAsync(new Rider
            {
                Id = riderId,
                LicenseType = LicenseType.A
            });
            _rentalRepoMock.Setup(r => r.AddAsync(It.IsAny<Rental>())).Returns(Task.CompletedTask);

            var request = new CreateRentalRequest
            {
                MotorcycleId = motoId,
                RiderId = riderId,
                Plan = RentalPlan.Days7,
                ExpectedEndDate = DateTime.UtcNow.Date.AddDays(7)
            };

            var result = await _service.CreateRentalAsync(request);

            result.Should().NotBeNull();
            result.MotorcycleId.Should().Be(motoId);
        }

        [Fact]
        public async Task ReturnRentalAsync_ShouldReturnRental_WhenValid()
        {
            var rentalId = Guid.NewGuid();
            _rentalRepoMock.Setup(r => r.GetByIdAsync(rentalId)).ReturnsAsync(new Rental { Id = rentalId });

            var returnDate = DateTime.UtcNow.Date;
            var result = await _service.ReturnRentalAsync(rentalId, returnDate);

            result.Should().NotBeNull();
            result.ActualEndDate.Should().Be(returnDate);
        }
    }
}

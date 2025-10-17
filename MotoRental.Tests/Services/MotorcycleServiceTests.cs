using Moq;
using FluentAssertions;
using MotoRental.Api.Application.Services;
using MotoRental.Api.Application.DTOs;
using MotoRental.Api.Infrastructure.Repositories;
using MotoRental.Api.Domain.Entities;
using MotoRental.Api.Exceptions;

namespace MotoRental.Tests.Services
{
    public class MotorcycleServiceTests
    {
        private readonly Mock<IMotorcycleRepository> _repoMock;
        private readonly MotorcyleService _service;

        public MotorcycleServiceTests()
        {
            _repoMock = new Mock<IMotorcycleRepository>();
            _service = new MotorcyleService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateMotorcycleAsync_ShouldCreateMotorcycle_WhenValid()
        {
            var request = new CreateMotorcycleRequest
            {
                Year = 2020,
                Model = "Fan",
                Plate = "RKLO823"
            };

            _repoMock.Setup(r => r.GetByPlateAsync(request.Plate)).ReturnsAsync((Motorcycle?)null);
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Motorcycle>())).Returns(Task.CompletedTask);

            var result = await _service.CreateMotorcycleAsync(request);

            result.Should().NotBeNull();
            result.Plate.Should().Be("RKLO823");
        }

        [Fact]
        public async Task CreateMotorcycleAsync_ShouldThrow_WhenPlateExists()
        {
            _repoMock.Setup(r => r.GetByPlateAsync("RKLO823"))
                     .ReturnsAsync(new Motorcycle { Plate = "RKLO823" });

            var request = new CreateMotorcycleRequest
            {
                Year = 2020,
                Model = "Fan",
                Plate = "RKLO823"
            };

            await Assert.ThrowsAsync<BusinessException>(() => _service.CreateMotorcycleAsync(request));
        }

        [Fact]
        public async Task GetMotorcyclesAsync_ShouldReturnList()
        {
            _repoMock.Setup(r => r.GetAllAsync(null))
                     .ReturnsAsync(new List<Motorcycle>
                     {
                         new Motorcycle { Id = Guid.NewGuid(), Year = 2020, Model = "Fan", Plate = "RKLO823" }
                     });

            var result = await _service.GetMotorcyclesAsync();

            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task UpdatePlateAsync_ShouldUpdate_WhenValid()
        {
            var motoId = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(motoId))
                     .ReturnsAsync(new Motorcycle { Id = motoId, Plate = "OLD123" });
            _repoMock.Setup(r => r.GetByPlateAsync("NEW123")).ReturnsAsync((Motorcycle?)null);
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Motorcycle>())).Returns(Task.CompletedTask);

            await _service.UpdatePlateAsync(motoId, "NEW123");

            _repoMock.Verify(r => r.UpdateAsync(It.Is<Motorcycle>(m => m.Plate == "NEW123")), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrow_WhenHasRentals()
        {
            var motoId = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(motoId)).ReturnsAsync(new Motorcycle { Id = motoId });
            _repoMock.Setup(r => r.HasRentalsAsync(motoId)).ReturnsAsync(true);

            await Assert.ThrowsAsync<BusinessException>(() => _service.DeleteAsync(motoId));
        }
    }
}

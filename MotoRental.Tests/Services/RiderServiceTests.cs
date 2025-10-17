using Moq;
using FluentAssertions;
using MotoRental.Api.Application.Services;
using MotoRental.Api.Application.DTOs;
using MotoRental.Api.Domain.Entities;
using MotoRental.Api.Enums;
using MotoRental.Infrastructure.Interfaces;

namespace MotoRental.Tests.Services
{
    public class RiderServiceTests
    {
        private readonly Mock<IRiderRepository> _repoMock;
        private readonly RiderService _service;

        public RiderServiceTests()
        {
            _repoMock = new Mock<IRiderRepository>();
            _service = new RiderService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateRiderAsync_ShouldCreate_WhenValid()
        {
            var request = new CreateRiderRequest
            {
                Name = "Joao",
                Cnpj = "123456",
                BirthDate = DateTime.UtcNow.AddYears(-25),
                CnhNumber = "CNH123",
                LicenseType = LicenseType.A
            };

            _repoMock.Setup(r => r.GetByCnpjAsync(request.Cnpj)).ReturnsAsync((Rider?)null);
            _repoMock.Setup(r => r.GetByCnhNumberAsync(request.CnhNumber)).ReturnsAsync((Rider?)null);
            _repoMock.Setup(r => r.AddAsync(It.IsAny<Rider>())).Returns(Task.CompletedTask);

            var result = await _service.CreateRiderAsync(request);

            result.Should().NotBeNull();
            result.Cnpj.Should().Be("123456");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnRider()
        {
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(new Rider { Id = id, Name = "Joao" });

            var result = await _service.GetByIdAsync(id);

            result.Should().NotBeNull();
            result!.Id.Should().Be(id);
        }
    }
}

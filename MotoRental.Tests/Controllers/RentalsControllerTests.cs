using Microsoft.AspNetCore.Mvc;
using Moq;
using MotoRental.Api.Application.DTOs;
using MotoRental.Api.Application.Services;
using MotoRental.Api.Controllers;
using MotoRental.Api.Enums;

namespace MotoRental.Tests.Controllers
{
    public class RentalsControllerTests
    {
        private readonly Mock<IRentalService> _serviceMock;
        private readonly RentalsController _controller;

        public RentalsControllerTests()
        {
            _serviceMock = new Mock<IRentalService>();
            _controller = new RentalsController(_serviceMock.Object);
        }

        [Fact]
        public async Task Create_ReturnsCreatedResult_WhenSuccessful()
        {
            var request = new CreateRentalRequest 
            { 
                RiderId = Guid.NewGuid(), 
                MotorcycleId = Guid.NewGuid() 
            };
            var dto = new RentalResponseDto 
            { 
                Id = Guid.NewGuid(),
                RiderId = request.RiderId,
                MotorcycleId = request.MotorcycleId,
                Plan = RentalPlan.Days7,
                StartDate = DateTime.UtcNow,
                ExpectedEndDate = DateTime.UtcNow.AddDays(3),
                PricePerDay = 100
            };

            _serviceMock.Setup(s => s.CreateRentalAsync(request)).ReturnsAsync(dto);

            var result = await _controller.Create(request);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(dto, createdResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenRentalDoesNotExist()
        {
            _serviceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((RentalResponseDto)null);

            var result = await _controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Devolucao_ReturnsOk_WhenSuccessful()
        {
            var dto = new RentalResponseDto
            {
                Id = Guid.NewGuid(),
                RiderId = Guid.NewGuid(),
                MotorcycleId = Guid.NewGuid(),
                Plan = RentalPlan.Days7,
                StartDate = DateTime.UtcNow,
                ExpectedEndDate = DateTime.UtcNow.AddDays(3),
                ActualEndDate = DateTime.UtcNow.AddDays(3),
                PricePerDay = 100
            };

            _serviceMock.Setup(s => s.ReturnRentalAsync(It.IsAny<Guid>(), It.IsAny<DateTime>())).ReturnsAsync(dto);

            var result = await _controller.Devolucao(Guid.NewGuid(), DateTime.UtcNow.AddDays(3));

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dto, okResult.Value);
        }
    }
}

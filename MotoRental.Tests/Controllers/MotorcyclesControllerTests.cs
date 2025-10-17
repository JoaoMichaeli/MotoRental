using Microsoft.AspNetCore.Mvc;
using Moq;
using MotoRental.Api.Controllers;
using MotoRental.Api.Exceptions;
using MotoRental.Application.Interfaces;
using MotoRental.Common.DTOs.Motorcycle.Response;

namespace MotoRental.Tests.Controllers
{
    public class MotorcyclesControllerTests
    {
        private readonly Mock<IMotorcycleService> _serviceMock;
        private readonly MotorcyclesController _controller;

        public MotorcyclesControllerTests()
        {
            _serviceMock = new Mock<IMotorcycleService>();
            _controller = new MotorcyclesController(_serviceMock.Object);
        }

        [Fact]
        public async Task Create_ReturnsCreatedResult_WhenSuccessful()
        {
            var request = new CreateMotorcycleRequest { Year = 2020, Model = "Fan", Plate = "RKLO823" };
            var dto = new MotorcycleResponseDto { Id = Guid.NewGuid(), Year = 2020, Model = "Fan", Plate = "RKLO823" };

            _serviceMock.Setup(s => s.CreateMotorcycleAsync(request)).ReturnsAsync(dto);

            var result = await _controller.Create(request);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(dto, createdResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsBadRequest_WhenBusinessException()
        {
            var request = new CreateMotorcycleRequest();
            _serviceMock.Setup(s => s.CreateMotorcycleAsync(request)).ThrowsAsync(new BusinessException("Erro"));

            var result = await _controller.Create(request);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Erro", ((dynamic)badRequest.Value).mensagem);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithList()
        {
            var motorcycles = new List<MotorcycleResponseDto>
            {
                new MotorcycleResponseDto { Id = Guid.NewGuid(), Year = 2020, Model = "Fan", Plate = "RKLO823" }
            };
            _serviceMock.Setup(s => s.GetMotorcyclesAsync(null)).ReturnsAsync(motorcycles);

            var result = await _controller.GetAll(null);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(motorcycles, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMotorcycleDoesNotExist()
        {
            _serviceMock.Setup(s => s.GetMotorcyclesAsync(null)).ReturnsAsync(new List<MotorcycleResponseDto>());

            var result = await _controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdatePlate_ReturnsNoContent_WhenSuccessful()
        {
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.UpdatePlateAsync(id, "NEW123")).Returns(Task.CompletedTask);

            var result = await _controller.UpdatePlate(id, "NEW123");

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.DeleteAsync(id)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(id);

            Assert.IsType<NoContentResult>(result);
        }
    }
}

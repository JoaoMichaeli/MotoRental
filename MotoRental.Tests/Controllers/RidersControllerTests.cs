using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MotoRental.Api.Controllers;
using MotoRental.Application.Interfaces;
using MotoRental.Common.DTOs.Rider.Response;

namespace MotoRental.Tests.Controllers
{
    public class RidersControllerTests
    {
        private readonly Mock<IRiderService> _serviceMock;
        private readonly RidersController _controller;

        public RidersControllerTests()
        {
            _serviceMock = new Mock<IRiderService>();
            _controller = new RidersController(_serviceMock.Object);
        }

        [Fact]
        public async Task Create_ReturnsCreatedResult_WhenSuccessful()
        {
            var request = new CreateRiderRequest { Name = "João", CnhNumber = "123456", Cnpj = "123456789" };
            var dto = new RiderResponseDto { Id = Guid.NewGuid(), Name = "João", CnhNumber = "123456", Cnpj = "123456789" };

            _serviceMock.Setup(s => s.CreateRiderAsync(request)).ReturnsAsync(dto);

            var result = await _controller.Create(request);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(dto, createdResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenRiderDoesNotExist()
        {
            _serviceMock.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((RiderResponseDto)null);

            var result = await _controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UploadCnh_ReturnsBadRequest_WhenFileIsNull()
        {
            var result = await _controller.UploadCnh(Guid.NewGuid(), null);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid CNH file.", ((dynamic)badRequest.Value).message);
        }

        [Fact]
        public async Task UploadCnh_ReturnsOk_WhenFileIsValid()
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.Length).Returns(10);

            var result = await _controller.UploadCnh(Guid.NewGuid(), fileMock.Object);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Contains("CNH uploaded successfully", ((dynamic)okResult.Value).message);
        }
    }
}

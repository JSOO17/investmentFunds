using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Application.Services.Interfaces;
using InvestmentFunds.Domain.Exceptions;
using InvestmentFunds.Infrastructure.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace InvestmentFunds.Infrastructure.Api.Tests.Controllers
{
    [TestClass]
    public class InvestmentFundControllerTests
    {
        private Mock<IInvestmentFundServices> _mockInvestmentFundService;
        private Mock<ILogger<InvestmentFundController>> _mockLogger;
        private InvestmentFundController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockInvestmentFundService = new Mock<IInvestmentFundServices>();
            _mockLogger = new Mock<ILogger<InvestmentFundController>>();
            _controller = new InvestmentFundController(_mockInvestmentFundService.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task Get_ShouldReturnOkResult_WithListOfInvestmentFunds()
        {
            // Arrange
            var mockResponse = new List<InvestmentFundResponse>
            {
                new InvestmentFundResponse { },
                new InvestmentFundResponse { }
            };
            _mockInvestmentFundService.Setup(service => service.GetAll()).ReturnsAsync(mockResponse);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual(mockResponse, okResult.Value);
        }

        [TestMethod]
        public async Task Get_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockInvestmentFundService.Setup(service => service.GetAll()).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.Get();

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnOkResult_WithInvestmentFund()
        {
            // Arrange
            var id = Guid.NewGuid();
            var mockResponse = new InvestmentFundResponse { };
            _mockInvestmentFundService.Setup(service => service.GetById(id)).ReturnsAsync(mockResponse);

            // Act
            var result = await _controller.Get(id);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual(mockResponse, okResult.Value);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnBadRequest_WhenResourceNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockInvestmentFundService.Setup(service => service.GetById(id)).ThrowsAsync(new ResourceNotFoundException());

            // Act
            var result = await _controller.Get(id);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockInvestmentFundService.Setup(service => service.GetById(id)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.Get(id);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
        }
    }
}

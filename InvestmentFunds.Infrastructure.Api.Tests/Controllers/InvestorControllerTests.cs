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
    public class InvestorControllerTests
    {
        private Mock<IInvestorServices> _mockInvestorServices;
        private Mock<ILogger<InvestorController>> _mockLogger;
        private InvestorController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockInvestorServices = new Mock<IInvestorServices>();
            _mockLogger = new Mock<ILogger<InvestorController>>();
            _controller = new InvestorController(_mockInvestorServices.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task Get_ShouldReturnOkResult_WhenInvestorIsFound()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var expectedResponse = new InvestorResponse { };

            _mockInvestorServices
                .Setup(s => s.GetAmmountById(testId))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Get(testId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual(expectedResponse, okResult.Value);
        }

        [TestMethod]
        public async Task Get_ShouldReturnBadRequestResult_WhenInvestorNotFound()
        {
            // Arrange
            var testId = Guid.NewGuid();
            _mockInvestorServices
                .Setup(s => s.GetAmmountById(testId))
                .ThrowsAsync(new ResourceNotFoundException());

            // Act
            var result = await _controller.Get(testId);

            var objectResult = result.Result as ObjectResult;
            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
            Assert.AreEqual($"Investor {testId} was not found.", objectResult.Value);
        }

        [TestMethod]
        public async Task Get_ShouldReturnInternalServerErrorResult_WhenAnExceptionOccurs()
        {
            // Arrange
            var testId = Guid.NewGuid();
            _mockInvestorServices
                .Setup(s => s.GetAmmountById(testId))
                .ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.Get(testId);

            // Assert
            var objectResult = result.Result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("Something was wrong", objectResult.Value);
        }
    }
}

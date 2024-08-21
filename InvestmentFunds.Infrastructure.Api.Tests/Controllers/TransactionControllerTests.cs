using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Application.Services.Interfaces;
using InvestmentFunds.Infrastructure.Api.Controllers;
using InvestmentFunds.Infrastructure.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentFunds.Infrastructure.Api.Tests.Controllers
{
    [TestClass]
    public class TransactionControllerTests
    {
        private Mock<ITransactionServices> _mockTransactionServices;
        private Mock<ILogger<TransactionController>> _mockLogger;
        private TransactionController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockTransactionServices = new Mock<ITransactionServices>();
            _mockLogger = new Mock<ILogger<TransactionController>>();
            _controller = new TransactionController(_mockTransactionServices.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task Get_ShouldReturnOkResult_WithListOfTransactions()
        {
            // Arrange
            var mockResponse = new List<TransactionResponse>
            {
                new TransactionResponse { },
                new TransactionResponse { }
            };
            _mockTransactionServices.Setup(service => service.GetAll()).ReturnsAsync(mockResponse);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result.Result as ObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual(mockResponse, okResult.Value);
        }

        [TestMethod]
        public async Task Get_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockTransactionServices.Setup(service => service.GetAll()).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.Get();

            // Assert
            var objectResult = result.Result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
            var response = objectResult.Value as ApiResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual("Something was wrong", response.Message);
        }
    }
}

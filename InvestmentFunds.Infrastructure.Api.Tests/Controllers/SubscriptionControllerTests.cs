using InvestmentFunds.Application.DTO.Request;
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
    public class SubscriptionControllerTests
    {
        private Mock<ISubscriptionServices> _mockSubscriptionServices;
        private Mock<ILogger<SubscriptionController>> _mockLogger;
        private SubscriptionController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockSubscriptionServices = new Mock<ISubscriptionServices>();
            _mockLogger = new Mock<ILogger<SubscriptionController>>();
            _controller = new SubscriptionController(_mockSubscriptionServices.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task Get_ShouldReturnOkResult_WithListOfSubscriptions()
        {
            // Arrange
            var mockResponse = new List<SubscriptionResponse>
            {
                new SubscriptionResponse { },
                new SubscriptionResponse { }
            };
            _mockSubscriptionServices.Setup(service => service.GetAll()).ReturnsAsync(mockResponse);

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
            _mockSubscriptionServices.Setup(service => service.GetAll()).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.Get();

            // Assert
            var objectResult = result.Result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("Something was wrong", objectResult.Value);
        }

        [TestMethod]
        public async Task Post_ShouldReturnCreated_WhenSubscriptionIsSuccessful()
        {
            // Arrange
            var request = new SubscriptionRequest { };
            _mockSubscriptionServices.Setup(service => service.Subscribe(request)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(request);

            // Assert
            var objectResult = result as StatusCodeResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.Created, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Post_ShouldReturnBadRequest_WhenResourceNotFound()
        {
            // Arrange
            var request = new SubscriptionRequest { };
            _mockSubscriptionServices.Setup(service => service.Subscribe(request)).ThrowsAsync(new ResourceNotFoundException());

            // Act
            var result = await _controller.Post(request);

            // Assert
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
            Assert.AreEqual($"InvestmentFund {request.InvestmentFundId} was not found.", objectResult.Value);
        }

        [TestMethod]
        public async Task Post_ShouldReturnBadRequest_WhenInvalidOperation()
        {
            // Arrange
            var request = new SubscriptionRequest { };
            _mockSubscriptionServices.Setup(service => service.Subscribe(request)).ThrowsAsync(new InvalidOperationException("Invalid operation"));

            // Act
            var result = await _controller.Post(request);

            // Assert
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
            Assert.AreEqual("Invalid operation", objectResult.Value);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnOk_WhenCancellationIsSuccessful()
        {
            // Arrange
            var request = new CancelRequest { };
            _mockSubscriptionServices.Setup(service => service.Cancel(request.Id, request.InvestorId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(request);

            // Assert
            var objectResult = result as OkResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.OK, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnBadRequest_WhenResourceNotFound()
        {
            // Arrange
            var request = new CancelRequest { };
            _mockSubscriptionServices.Setup(service => service.Cancel(request.Id, request.InvestorId)).ThrowsAsync(new ResourceNotFoundException());

            // Act
            var result = await _controller.Delete(request);

            // Assert
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, objectResult.StatusCode);
            Assert.AreEqual($"Subscription {request.Id} was not found.", objectResult.Value);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnBadRequest_WhenInvalidOperation()
        {
            // Arrange
            var request = new CancelRequest { };
            _mockSubscriptionServices.Setup(service => service.Cancel(request.Id, request.InvestorId)).ThrowsAsync(new InvalidOperationException("Invalid operation"));

            // Act
            var result = await _controller.Delete(request);

            // Assert
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, objectResult.StatusCode);
            Assert.AreEqual("Invalid operation", objectResult.Value);
        }

        [TestMethod]
        public async Task Delete_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var request = new CancelRequest { };
            _mockSubscriptionServices.Setup(service => service.Cancel(request.Id, request.InvestorId)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.Delete(request);

            // Assert
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
            Assert.AreEqual("Something was wrong", objectResult.Value);
        }
    }
}

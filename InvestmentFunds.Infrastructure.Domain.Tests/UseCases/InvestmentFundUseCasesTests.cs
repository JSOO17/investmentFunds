using InvestmentFunds.Domain.Exceptions;
using InvestmentFunds.Domain.Interfaces.SPI;
using InvestmentFunds.Domain.Models;
using InvestmentFunds.Domain.UseCases;
using Moq;

namespace InvestmentFunds.Domain.Tests.UseCases
{
    [TestClass]
    public class InvestmentFundUseCasesTests
    {
        [TestMethod]
        public async Task GetAll_ShouldCallPersistencePortGetAll()
        {
            // Arrange
            var mockPersistencePort = new Mock<IInvestmentFundPersistencePort>();
            var useCases = new InvestmentFundUseCases(mockPersistencePort.Object);

            // Act
            await useCases.GetAll();

            // Assert
            mockPersistencePort.Verify(p => p.GetAll(), Times.Once);
        }

        [TestMethod]
        public async Task GetById_ShouldCallPersistencePortGetById_AndReturnInvestmentFund()
        {
            // Arrange
            var expectedInvestmentFund = new InvestmentFundModel { Id = Guid.NewGuid() };
            var mockPersistencePort = new Mock<IInvestmentFundPersistencePort>();
            mockPersistencePort.Setup(p => p.GetById(It.IsAny<Guid>())).ReturnsAsync(expectedInvestmentFund);
            var useCases = new InvestmentFundUseCases(mockPersistencePort.Object);

            // Act
            var result = await useCases.GetById(expectedInvestmentFund.Id);

            // Assert
            Assert.AreEqual(expectedInvestmentFund, result);
        }

        [TestMethod]
        public async Task GetById_ShouldThrowResourceNotFoundException_WhenInvestmentFundNotFound()
        {
            // Arrange
            var mockPersistencePort = new Mock<IInvestmentFundPersistencePort>();
            mockPersistencePort.Setup(p => p.GetById(It.IsAny<Guid>())).ReturnsAsync(null as InvestmentFundModel);
            var useCases = new InvestmentFundUseCases(mockPersistencePort.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ResourceNotFoundException>(() => useCases.GetById(Guid.NewGuid()));
        }
    }
}
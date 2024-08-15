using InvestmentFunds.Domain.Interfaces.SPI;
using InvestmentFunds.Domain.UseCases;
using Moq;

namespace InvestmentFunds.Domain.Tests.UseCases
{
    [TestClass]
    public class InvestorUseCasesTests
    {
        private Mock<IInvestorPersistencePort> _mockInvestorPersistencePort;
        private InvestorUseCases _investorUseCases;

        [TestInitialize]
        public void Setup()
        {
            _mockInvestorPersistencePort = new Mock<IInvestorPersistencePort>();
            _investorUseCases = new InvestorUseCases(_mockInvestorPersistencePort.Object);
        }

        [TestMethod]
        public async Task GetAmmountById_ShouldReturnAmount_WhenIdIsValid()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var expectedAmount = 100.00m;

            // Configurar el mock para devolver un valor esperado
            _mockInvestorPersistencePort
                .Setup(p => p.GetAmmountById(testId))
                .ReturnsAsync(expectedAmount);

            // Act
            var result = await _investorUseCases.GetAmmountById(testId);

            // Assert
            Assert.AreEqual(expectedAmount, result);
        }
    }
}

using InvestmentFunds.Domain.Interfaces.SPI;
using InvestmentFunds.Domain.Models;
using InvestmentFunds.Domain.UseCases;
using Moq;

namespace InvestmentFunds.Domain.Tests.UseCases
{
    [TestClass]
    public class TransactionUseCasesTests
    {
        private Mock<ITransactionPersistencePort> _mockPersistencePort;
        private TransactionUseCases _useCases;

        [TestInitialize]
        public void SetUp()
        {
            _mockPersistencePort = new Mock<ITransactionPersistencePort>();
            _useCases = new TransactionUseCases(_mockPersistencePort.Object);
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnListOfTransactions()
        {
            // Arrange
            var transactions = new List<TransactionResponseModel>
            {
                new TransactionResponseModel { },
                new TransactionResponseModel { }
            };

            _mockPersistencePort.Setup(m => m.GetAll())
                .ReturnsAsync(transactions);

            // Act
            var result = await _useCases.GetAll();

            // Assert
            Assert.AreEqual(transactions.Count, result.Count);
            CollectionAssert.AreEquivalent(transactions, result);
        }
    }
}

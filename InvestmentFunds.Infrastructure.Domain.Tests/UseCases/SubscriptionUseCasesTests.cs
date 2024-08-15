using InvestmentFunds.Domain.Exceptions;
using InvestmentFunds.Domain.Interfaces.SPI;
using InvestmentFunds.Domain.Models;
using InvestmentFunds.Domain.UseCases;
using InvestmentFunds.Domain.Utils;
using Moq;

namespace InvestmentFunds.Domain.Tests.UseCases
{
    [TestClass]
    public class SubscriptionUseCasesTests
    {
        private Mock<IInvestorPersistencePort> _mockInvestorPersistence;
        private Mock<ISubscriptionPersistencePort> _mockSubscriptionPersistence;
        private Mock<IInvestmentFundPersistencePort> _mockInvestmentFundPersistence;
        private Mock<ITransactionPersistencePort> _mockTransactionPersistence;
        private SubscriptionUseCases _subscriptionUseCases;

        [TestInitialize]
        public void Setup()
        {
            _mockInvestorPersistence = new Mock<IInvestorPersistencePort>();
            _mockSubscriptionPersistence = new Mock<ISubscriptionPersistencePort>();
            _mockInvestmentFundPersistence = new Mock<IInvestmentFundPersistencePort>();
            _mockTransactionPersistence = new Mock<ITransactionPersistencePort>();
            _subscriptionUseCases = new SubscriptionUseCases(
                _mockInvestorPersistence.Object,
                _mockSubscriptionPersistence.Object,
                _mockInvestmentFundPersistence.Object,
                _mockTransactionPersistence.Object
            );
        }

        [TestMethod]
        public async Task Subscribe_ShouldThrowException_WhenInvestorAmountIsInsufficient()
        {
            // Arrange
            var subscriptionModel = new SubscriptionModel
            {
                InvestorId = Guid.NewGuid(),
                InvestmentFundId = Guid.NewGuid(),
                AmountPayment = 100
            };

            _mockInvestorPersistence.Setup(x => x.GetAmmountById(subscriptionModel.InvestorId))
                .ReturnsAsync(50);

            _mockInvestmentFundPersistence.Setup(x => x.GetById(subscriptionModel.InvestmentFundId))
                .ReturnsAsync(new InvestmentFundModel { MinimumPayment = 10, Name = "Fund" });

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                _subscriptionUseCases.Subscribe(subscriptionModel));
        }

        [TestMethod]
        public async Task Subscribe_ShouldCreateSubscriptionAndUpdateEntities()
        {
            // Arrange
            var investmentId = Guid.NewGuid();
            var subscriptionModel = new SubscriptionModel
            {
                InvestorId = Guid.NewGuid(),
                InvestmentFundId = investmentId,
                AmountPayment = 100
            };

            _mockInvestorPersistence.Setup(x => x.GetAmmountById(subscriptionModel.InvestorId))
                .ReturnsAsync(200);

            _mockInvestmentFundPersistence.Setup(x => x.GetById(subscriptionModel.InvestmentFundId))
                .ReturnsAsync(new InvestmentFundModel { Id = investmentId, MinimumPayment = 10, Name = "Fund" });

            _mockSubscriptionPersistence.Setup(x => x.Create(subscriptionModel))
                .Returns(Task.CompletedTask);

            _mockInvestorPersistence.Setup(x => x.Update(subscriptionModel.InvestorId, 100))
                .Returns(Task.CompletedTask);

            _mockTransactionPersistence.Setup(x => x.Create(It.IsAny<TransactionModel>()))
                .Returns(Task.CompletedTask);

            _mockInvestmentFundPersistence.Setup(x => x.UpdateState(subscriptionModel.InvestmentFundId, InvestmentFundStates.Subscribed))
                .Returns(Task.CompletedTask);

            // Act
            await _subscriptionUseCases.Subscribe(subscriptionModel);

            // Assert
            _mockSubscriptionPersistence.Verify(x => x.Create(subscriptionModel), Times.Once);
            _mockInvestorPersistence.Verify(x => x.Update(subscriptionModel.InvestorId, 100), Times.Once);
            _mockTransactionPersistence.Verify(x => x.Create(It.IsAny<TransactionModel>()), Times.Once);
            _mockInvestmentFundPersistence.Verify(x => x.UpdateState(investmentId, InvestmentFundStates.Subscribed), Times.Once);
        }

        [TestMethod]
        public async Task Cancel_ShouldThrowException_WhenSubscriptionNotFound()
        {
            // Arrange
            var subscriptionId = Guid.NewGuid();
            var investorId = Guid.NewGuid();

            _mockSubscriptionPersistence.Setup(x => x.GetById(subscriptionId))
                .ReturnsAsync((SubscriptionModel)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ResourceNotFoundException>(() =>
                _subscriptionUseCases.Cancel(subscriptionId, investorId));
        }

        [TestMethod]
        public async Task Cancel_ShouldUpdateEntities_WhenSubscriptionIsCanceled()
        {
            // Arrange
            var subscriptionId = Guid.NewGuid();
            var investorId = Guid.NewGuid();
            var subscription = new SubscriptionModel
            {
                AmountPayment = 100,
                InvestmentFundId = Guid.NewGuid()
            };

            _mockSubscriptionPersistence.Setup(x => x.GetById(subscriptionId))
                .ReturnsAsync(subscription);

            _mockInvestorPersistence.Setup(x => x.GetAmmountById(investorId))
                .ReturnsAsync(200);

            _mockSubscriptionPersistence.Setup(x => x.Delete(subscriptionId))
                .Returns(Task.CompletedTask);

            _mockInvestorPersistence.Setup(x => x.Update(investorId, 300))
                .Returns(Task.CompletedTask);

            _mockTransactionPersistence.Setup(x => x.Create(It.IsAny<TransactionModel>()))
                .Returns(Task.CompletedTask);

            _mockInvestmentFundPersistence.Setup(x => x.UpdateState(subscription.InvestmentFundId, InvestmentFundStates.Open))
                .Returns(Task.CompletedTask);

            // Act
            await _subscriptionUseCases.Cancel(subscriptionId, investorId);

            // Assert
            _mockSubscriptionPersistence.Verify(x => x.Delete(subscriptionId), Times.Once);
            _mockInvestorPersistence.Verify(x => x.Update(investorId, 300), Times.Once);
            _mockTransactionPersistence.Verify(x => x.Create(It.IsAny<TransactionModel>()), Times.Once);
            _mockInvestmentFundPersistence.Verify(x => x.UpdateState(subscription.InvestmentFundId, InvestmentFundStates.Open), Times.Once);
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnSubscriptions()
        {
            // Arrange
            var subscriptions = new List<SubscriptionResponseModel>
            {
                new SubscriptionResponseModel { },
                new SubscriptionResponseModel { }
            };

            _mockSubscriptionPersistence.Setup(x => x.GetAll())
                .ReturnsAsync(subscriptions);

            // Act
            var result = await _subscriptionUseCases.GetAll();

            // Assert
            Assert.AreEqual(subscriptions.Count, result.Count);
        }
    }
}

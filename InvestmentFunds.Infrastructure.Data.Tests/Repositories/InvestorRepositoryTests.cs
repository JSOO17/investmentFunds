//using InvestmentFunds.Domain.Models;
//using InvestmentFunds.Infrastructure.Data.Repositories;
//using MongoDB.Driver;
//using Moq;
//using System.Linq.Expressions;

//namespace InvestmentFunds.Infrastructure.Data.Tests.Repositories
//{
//    [TestClass]
//    public class InvestorRepositoryTests
//    {
//        private Mock<IMongoCollection<Investor>> _mockInvestorCollection;

//        [TestInitialize]
//        public void Initialize()
//        {
//            _mockInvestorCollection = new Mock<IMongoCollection<Investor>>();
//        }

//        [TestMethod]
//        public async Task GetAmmountById_ShouldReturnCorrectAmount_WhenInvestorExists()
//        {
//            // Arrange
//            var expectedId = Guid.NewGuid();
//            var expectedAmount = 1000.00m;
//            var mockInvestor = new Investor { Id = expectedId, Amount = expectedAmount };

//            _mockInvestorCollection.Setup(c => c.Find(It.IsAny<Expression<Func<Investor, bool>>>()))
//                .Returns(Mock.Of<IFindFluent<Investor, Investor>>(m => m.FirstOrDefaultAsync() == Task.FromResult(mockInvestor)));

//            var investorRepository = new InvestorRepository(_mockInvestorCollection.Object);

//            // Act
//            var actualAmount = await investorRepository.GetAmmountById(expectedId);

//            // Assert
//            Assert.AreEqual(expectedAmount, actualAmount);
//        }

//        [TestMethod]
//        public async Task GetAmmountById_ShouldReturnZero_WhenInvestorDoesNotExist()
//        {
//            // Arrange
//            var expectedId = Guid.NewGuid();

//            _mockInvestorCollection.Setup(c => c.Find(It.IsAny<Expression<Func<Investor, bool>>>()))
//                .Returns(Mock.Of<IFindFluent<Investor, Investor>>(m => m.FirstOrDefaultAsync() == Task.FromResult<Investor>(null)));

//            var investorRepository = new InvestorRepository(_mockInvestorCollection.Object);

//            // Act
//            var actualAmount = await investorRepository.GetAmmountById(expectedId);

//            // Assert
//            Assert.AreEqual(0m, actualAmount);
//        }

//        [TestMethod]
//        public async Task Update_ShouldUpdateAmount_WhenInvestorExists()
//        {
//            // Arrange
//            var expectedId = Guid.NewGuid();
//            var initialAmount = 500.00m;
//            var updatedAmount = 1500.00m;
//            var mockInvestor = new Investor { Id = expectedId, Amount = initialAmount };

//            _mockInvestorCollection.Setup(c => c.Find(It.IsAny<Expression<Func<Investor, bool>>>()))
//                .Returns(Mock.Of<IFindFluent<Investor, Investor>>(m => m.FirstOrDefaultAsync() == Task.FromResult(mockInvestor)));

//            var investorRepository = new InvestorRepository(_mockInvestorCollection.Object);

//            // Act
//            await investorRepository.Update(expectedId, updatedAmount);

//            // Assert
//            _mockInvestorCollection.Verify(c => c.UpdateOneAsync(It.IsAny<FilterDefinition<Investor>>(), It.IsAny<UpdateDefinition<Investor>>()));
//        }

//        [TestMethod]
//        public async Task Update_ShouldThrowException_WhenInvestorDoesNotExist()
//        {
//            // Arrange
//            var expectedId = Guid.NewGuid();
//            var updatedAmount = 1500.00m;

//            _mockInvestorCollection.Setup(c => c.Find(It.IsAny<Expression<Func<Investor, bool>>>()))
//                .Returns(Mock.Of<IFindFluent<Investor, Investor>>(m => m.FirstOrDefaultAsync() == Task.FromResult<Investor>(null)));

//            var investorRepository = new InvestorRepository(_mockInvestorCollection.Object);

//            // Act & Assert
//            await Assert.ThrowsExceptionAsync<Exception>(async () => await investorRepository.Update(expectedId, updatedAmount));
//        }
//    }
//}
using InvestmentFunds.Domain.Interfaces.SPI;
using InvestmentFunds.Domain.Models;
using InvestmentFunds.Infrastructure.Data.Mappers;
using InvestmentFunds.Infrastructure.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InvestmentFunds.Infrastructure.Data.Repositories
{
    public class TransactionRepository : ITransactionPersistencePort
    {
        private readonly IMongoCollection<Transaction> _transactionCollection;
        private readonly TransactionMapper _transactionMapper;

        public TransactionRepository(IMongoCollection<Transaction> transactionCollection, TransactionMapper transactionMapper)
        {
            _transactionCollection = transactionCollection;
            _transactionMapper = transactionMapper;
        }

        public async Task Create(TransactionModel transaction)
        {
            await _transactionCollection.InsertOneAsync(_transactionMapper.ToTransaction(transaction));
        }

        public async Task<List<TransactionResponseModel>> GetAll()
        {
            var pipeline = BuildPipelineInvestmentFundDetails();

            var result = await _transactionCollection.Aggregate<TransactionWithFund>(pipeline).ToListAsync();

            return _transactionMapper.ToModel(result);
        }

        private static BsonDocument[] BuildPipelineInvestmentFundDetails()
        {
            var lookupStage = new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "investmentFund" },
                { "localField", "InvestmentFundId" },
                { "foreignField", "_id" },
                { "as", "InvestmentFundDetails" }

            });

            var unwindStage = new BsonDocument("$unwind", "$InvestmentFundDetails");

            return new[] { lookupStage, unwindStage };
        }
    }
}

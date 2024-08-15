using InvestmentFunds.Domain.Interfaces.SPI;
using InvestmentFunds.Domain.Models;
using InvestmentFunds.Infrastructure.Data.Mappers;
using InvestmentFunds.Infrastructure.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace InvestmentFunds.Infrastructure.Data.Repositories
{
    public class SubscriptionRepository : ISubscriptionPersistencePort
    {
        private readonly IMongoCollection<Subscription> _subscriptionCollection;
        private readonly IMongoCollection<InvestmentFund> _investmentFundCollection;
        private readonly SubscriptionMapper _subscriptionMapper;

        public SubscriptionRepository(IMongoDatabase database, SubscriptionMapper subscriptionMapper)
        {
            _subscriptionCollection = database.GetCollection<Subscription>("subscriptions");
            _investmentFundCollection = database.GetCollection<InvestmentFund>("investmentFunds");
            _subscriptionMapper = subscriptionMapper;
        }

        public async Task Create(SubscriptionModel model)
        {
            var subscription = _subscriptionMapper.ToSubscription(model);

            await _subscriptionCollection.InsertOneAsync(subscription);
        }

        public async Task Delete(Guid id)
        {
            await _subscriptionCollection.DeleteOneAsync(BuildFilterById(id)).ConfigureAwait(false);
        }

        public async Task<List<SubscriptionResponseModel>> GetAll()
        {
            var pipeline = new[]
            {
                new BsonDocument("$lookup", new BsonDocument
                {
                    { "from", "investmentFund" },
                    { "localField", "InvestmentFundId" },
                    { "foreignField", "_id" },
                    { "as", "InvestmentFundDetails" }
                }),

                new BsonDocument("$unwind", "$InvestmentFundDetails"),
            };

            var result = await _subscriptionCollection.Aggregate<SubscriptionWithFund>(pipeline).ToListAsync();


            return _subscriptionMapper.ToModel(result);
        }

        public async Task<SubscriptionModel> GetById(Guid id)
        {
            var result = await _subscriptionCollection.Find(s => s.Id == id).FirstOrDefaultAsync() ?? throw new Exception();

            return _subscriptionMapper.ToModel(result);
        }

        private FilterDefinition<Subscription> BuildFilterById(Guid id)
        {
            return Builders<Subscription>.Filter.Eq(s => s.Id, id);
        }
    }
}

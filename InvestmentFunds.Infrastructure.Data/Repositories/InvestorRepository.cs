using InvestmentFunds.Domain.Interfaces.SPI;
using InvestmentFunds.Domain.Models;
using MongoDB.Driver;

namespace InvestmentFunds.Infrastructure.Data.Repositories
{
    public class InvestorRepository : IInvestorPersistencePort
    {
        private readonly IMongoCollection<Investor> _investorCollection;

        public InvestorRepository(IMongoCollection<Investor> investorCollection)
        {
            _investorCollection = investorCollection;
        }

        public async Task<decimal> GetAmmountById(Guid id)
        {
            return (await _investorCollection.Find(i => i.Id == id).FirstOrDefaultAsync()).Amount;
        }

        public async Task Update(Guid id, decimal amount)
        {
            var filter = Builders<Investor>.Filter.Eq(c => c.Id, id);
            var update = Builders<Investor>.Update.Set(c => c.Amount, amount);

            await _investorCollection.UpdateOneAsync(filter, update);
        }
    }
}

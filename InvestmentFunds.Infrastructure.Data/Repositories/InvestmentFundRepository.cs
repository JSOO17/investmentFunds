using InvestmentFunds.Domain.Exceptions;
using InvestmentFunds.Domain.Interfaces.SPI;
using InvestmentFunds.Domain.Models;
using InvestmentFunds.Infrastructure.Data.Mappers;
using MongoDB.Driver;
using static MongoDB.Driver.WriteConcern;

namespace InvestmentFunds.Infrastructure.Data.Repositories
{
    public class InvestmentFundRepository : IInvestmentFundPersistencePort
    {
        private readonly IMongoCollection<InvestmentFund> _investmentFundCollection;
        private readonly InvestmentFundMapper _investmentFundMapper;

        public InvestmentFundRepository(IMongoCollection<InvestmentFund> investmentFundCollection, InvestmentFundMapper investmentFundMapper)
        {
            _investmentFundCollection = investmentFundCollection;
            _investmentFundMapper = investmentFundMapper; 
        }

        public async Task<List<InvestmentFundModel>> GetAll()
        {
            var result = await _investmentFundCollection.Find(_ => true).ToListAsync();
            return _investmentFundMapper.ToModel(result);
        }

        public async  Task<InvestmentFundModel?> GetById(Guid id)
        {
            var result = await _investmentFundCollection.Find(s => s.Id == id).FirstOrDefaultAsync();

            return _investmentFundMapper.ToModel(result);
        }

        public async Task UpdateState(Guid id, string state)
        {
            var filter = Builders<InvestmentFund>.Filter.Eq(c => c.Id, id);
            var update = Builders<InvestmentFund>.Update.Set(c => c.State, state);

            await _investmentFundCollection.UpdateOneAsync(filter, update);
        }
    }
}

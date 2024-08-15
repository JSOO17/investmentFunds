using InvestmentFunds.Domain.Exceptions;
using InvestmentFunds.Domain.Interfaces.API;
using InvestmentFunds.Domain.Interfaces.SPI;
using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Domain.UseCases
{
    public class InvestmentFundUseCases : IInvestmentFundServicePort
    {
        private readonly IInvestmentFundPersistencePort _persistencePort;

        public InvestmentFundUseCases(IInvestmentFundPersistencePort persistencePort) => _persistencePort = persistencePort;

        public async Task<List<InvestmentFundModel>> GetAll() => await _persistencePort.GetAll();

        public async Task<InvestmentFundModel> GetById(Guid id) => await _persistencePort.GetById(id) ?? throw new ResourceNotFoundException();
    }
}

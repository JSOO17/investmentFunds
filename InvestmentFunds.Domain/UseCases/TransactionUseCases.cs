using InvestmentFunds.Domain.Interfaces.API;
using InvestmentFunds.Domain.Interfaces.SPI;
using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Domain.UseCases
{
    public class TransactionUseCases : ITransactionServicePort
    {
        private readonly ITransactionPersistencePort _persistencePort;

        public TransactionUseCases(ITransactionPersistencePort persistencePort) => _persistencePort = persistencePort;

        public async Task<List<TransactionResponseModel>> GetAll() => await _persistencePort.GetAll();
    }
}

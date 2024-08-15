using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Domain.Interfaces.SPI
{
    public interface ITransactionPersistencePort
    {
        Task<List<TransactionResponseModel>> GetAll();
        Task Create(TransactionModel transaction);
    }
}

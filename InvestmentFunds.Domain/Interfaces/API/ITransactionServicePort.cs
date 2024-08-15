using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Domain.Interfaces.API
{
    public interface ITransactionServicePort
    {
        Task<List<TransactionResponseModel>> GetAll();
    }
}

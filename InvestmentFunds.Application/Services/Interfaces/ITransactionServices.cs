using InvestmentFunds.Application.DTO.Response;

namespace InvestmentFunds.Application.Services.Interfaces
{
    public interface ITransactionServices
    {
        Task<List<TransactionResponse>> GetAll();
    }
}

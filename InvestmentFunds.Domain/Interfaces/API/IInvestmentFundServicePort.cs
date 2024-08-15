using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Domain.Interfaces.API
{
    public interface IInvestmentFundServicePort
    {
        Task<List<InvestmentFundModel>> GetAll();
        Task<InvestmentFundModel> GetById(Guid id);
    }
}

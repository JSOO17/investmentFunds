using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Domain.Interfaces.SPI
{
    public interface IInvestmentFundPersistencePort
    {
        Task<List<InvestmentFundModel>> GetAll();
        Task<InvestmentFundModel?> GetById(Guid id);
        Task UpdateState(Guid id, string state);
    }
}

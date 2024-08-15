using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Domain.Interfaces.SPI
{
    public interface ISubscriptionPersistencePort
    {
        Task<List<SubscriptionResponseModel>> GetAll();
        Task Create(SubscriptionModel model);
        Task<SubscriptionModel> GetById(Guid id);
        Task Delete(Guid id);
    }
}

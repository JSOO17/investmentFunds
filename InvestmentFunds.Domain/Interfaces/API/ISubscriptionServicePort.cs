using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Domain.Interfaces.API
{
    public interface ISubscriptionServicePort
    {
        Task<List<SubscriptionResponseModel>> GetAll();
        Task Subscribe(SubscriptionModel model);
        Task Cancel(Guid id, Guid investorId);
    }
}

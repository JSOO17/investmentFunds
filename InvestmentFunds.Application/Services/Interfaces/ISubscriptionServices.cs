using InvestmentFunds.Application.DTO.Request;
using InvestmentFunds.Application.DTO.Response;

namespace InvestmentFunds.Application.Services.Interfaces
{
    public interface ISubscriptionServices
    {
        Task<List<SubscriptionResponse>> GetAll();
        Task Subscribe(SubscriptionRequest request);
        Task Cancel(Guid id, Guid investorId);
    }
}

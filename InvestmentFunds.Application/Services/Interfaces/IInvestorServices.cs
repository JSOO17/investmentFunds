using InvestmentFunds.Application.DTO.Response;

namespace InvestmentFunds.Application.Services.Interfaces
{
    public interface IInvestorServices
    {
        Task<InvestorResponse> GetAmmountById(Guid id);
    }
}

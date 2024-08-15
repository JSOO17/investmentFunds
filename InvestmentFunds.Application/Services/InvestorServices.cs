using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Application.Services.Interfaces;
using InvestmentFunds.Domain.Interfaces.API;

namespace InvestmentFunds.Application.Services
{
    public class InvestorServices : IInvestorServices
    {
        private readonly IInvestorServicePort _servicePort;

        public InvestorServices(IInvestorServicePort servicePort)
        {
            _servicePort = servicePort;
        }

        public async Task<InvestorResponse> GetAmmountById(Guid id)
        {
            return new InvestorResponse
            {
                Amount = await _servicePort.GetAmmountById(id)
            };
        }
    }
}

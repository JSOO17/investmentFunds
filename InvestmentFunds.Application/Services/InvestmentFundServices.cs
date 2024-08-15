using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Application.Mappers;
using InvestmentFunds.Application.Services.Interfaces;
using InvestmentFunds.Domain.Interfaces.API;

namespace InvestmentFunds.Application.Services
{
    public class InvestmentFundServices : IInvestmentFundServices
    {
        private readonly IInvestmentFundServicePort _servicePort;
        private readonly InvestmentFundMapper _mapper;

        public InvestmentFundServices(IInvestmentFundServicePort servicePort, InvestmentFundMapper mapper)
        {
            _servicePort = servicePort;
            _mapper = mapper;
        }

        public async Task<List<InvestmentFundResponse>> GetAll()
        {
            return _mapper.ToResponse(await _servicePort.GetAll());
        }

        public async Task<InvestmentFundResponse> GetById(Guid id)
        {
            return _mapper.ToResponse(await _servicePort.GetById(id));
        }
    }
}

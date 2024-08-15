using InvestmentFunds.Application.DTO.Request;
using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Application.Mappers;
using InvestmentFunds.Application.Services.Interfaces;
using InvestmentFunds.Domain.Interfaces.API;

namespace InvestmentFunds.Application.Services
{
    public class SubscriptionServices : ISubscriptionServices
    {
        private readonly ISubscriptionServicePort _servicePort;
        private readonly SubscriptionMapper _mapper;

        public SubscriptionServices(ISubscriptionServicePort servicePort, SubscriptionMapper mapper)
        {
            _servicePort = servicePort;
            _mapper = mapper;
        }

        public async Task Cancel(Guid id, Guid investorId)
        {
            await _servicePort.Cancel(id, investorId);
        }

        public async Task<List<SubscriptionResponse>> GetAll()
        {
            var result = await _servicePort.GetAll();
            return _mapper.ToResponse(result);
        }

        public async Task Subscribe(SubscriptionRequest request)
        {
            await _servicePort.Subscribe(_mapper.ToModel(request));
        }
    }
}

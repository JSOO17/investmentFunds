using AutoMapper;
using InvestmentFunds.Application.DTO.Request;
using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Application.Mappers
{
    public class SubscriptionMapper
    {
        private readonly IMapper _mapper;

        public SubscriptionMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<SubscriptionResponse> ToResponse(List<SubscriptionResponseModel> models)
        {
            return _mapper.Map<List<SubscriptionResponse>>(models);
        }

        public SubscriptionModel ToModel(SubscriptionRequest request)
        {
            return _mapper.Map<SubscriptionModel>(request);
        }
    }
}

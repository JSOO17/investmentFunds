using AutoMapper;
using InvestmentFunds.Application.DTO.Request;
using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Application.Mappers
{
    public class TransactionMapper
    {
        private readonly IMapper _mapper;

        public TransactionMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<TransactionResponse> ToResponse(List<TransactionResponseModel> models)
        {
            return _mapper.Map<List<TransactionResponse>>(models);
        }

        public SubscriptionModel ToModel(SubscriptionRequest request)
        {
            return _mapper.Map<SubscriptionModel>(request);
        }
    }
}

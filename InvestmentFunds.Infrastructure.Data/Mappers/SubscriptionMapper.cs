using AutoMapper;
using InvestmentFunds.Domain.Models;
using InvestmentFunds.Infrastructure.Data.Models;

namespace InvestmentFunds.Infrastructure.Data.Mappers
{
    public class SubscriptionMapper
    {
        private readonly IMapper _mapper;

        public SubscriptionMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public SubscriptionResponseModel ToModel(SubscriptionWithFund subscription)
        {
            return _mapper.Map<SubscriptionResponseModel>(subscription);
        }

        public List<SubscriptionResponseModel> ToModel(List<SubscriptionWithFund> subscriptions)
        {
            return _mapper.Map<List<SubscriptionResponseModel>>(subscriptions);
        }

        public Subscription ToSubscription(SubscriptionModel subscriptionModel)
        {
            return _mapper.Map<Subscription>(subscriptionModel);
        }

        public SubscriptionModel ToModel(Subscription subscription)
        {
            return _mapper.Map<SubscriptionModel>(subscription);
        }
    }
}

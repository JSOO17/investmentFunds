using InvestmentFunds.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace InvestmentFunds.Infrastructure.Data.Models
{
    public class SubscriptionWithFund : Subscription
    {
        [BsonElement("InvestmentFundDetails")]
        public InvestmentFund InvestmentFundDetails { get; set; }
    }
}

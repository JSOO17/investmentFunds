using InvestmentFunds.Domain.Utils;
using MongoDB.Bson.Serialization.Attributes;

namespace InvestmentFunds.Domain.Models
{
    public class InvestmentFund
    {
        [BsonId]
        public Guid Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;
        [BsonElement("MinimumPayment")]
        public decimal MinimumPayment { get; set; }
        [BsonElement("Category")]
        public string Category { get; set; } = string.Empty;
        [BsonElement("State")]
        public string State { get; set; } = InvestmentFundStates.Open;
    }
}
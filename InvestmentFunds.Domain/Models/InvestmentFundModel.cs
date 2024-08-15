using InvestmentFunds.Domain.Utils;

namespace InvestmentFunds.Domain.Models
{
    public class InvestmentFundModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal MinimumPayment { get; set; }
        public string Category { get; set; } = string.Empty;
        public string State { get; set; } = InvestmentFundStates.Open;
    }
}
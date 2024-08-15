using InvestmentFunds.Domain.Utils;

namespace InvestmentFunds.Application.DTO.Response
{
    public class InvestmentFundResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal MinimumPayment { get; set; }
        public string Category { get; set; } = string.Empty;
        public string State { get; set; } = InvestmentFundStates.Open;
    }
}

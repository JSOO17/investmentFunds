namespace InvestmentFunds.Application.DTO.Request
{
    public class SubscriptionRequest
    {
        public Guid InvestorId { get; set; }
        public Guid InvestmentFundId { get; set; }
        public decimal AmountPayment { get; set; }
    }
}

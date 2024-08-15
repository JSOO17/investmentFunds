namespace InvestmentFunds.Domain.Models
{
    public class SubscriptionResponseModel
    {
        public Guid Id { get; set; }
        public InvestmentFundModel InvestmentFundDetails { get; set; }
        public decimal AmountPayment { get; set; }
    }
}

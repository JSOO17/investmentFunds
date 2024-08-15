namespace InvestmentFunds.Application.DTO.Response
{
    public class SubscriptionResponse
    {
        public Guid Id { get; set; }
        public InvestmentFundResponse InvestmentFundDetails { get; set; }
        public decimal AmountPayment { get; set; }
    }
}

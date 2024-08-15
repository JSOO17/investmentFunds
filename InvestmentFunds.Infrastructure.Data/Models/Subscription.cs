namespace InvestmentFunds.Domain.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public Guid InvestorId { get; set; }
        public Guid InvestmentFundId { get; set; }
        public decimal AmountPayment { get; set; }
    }
}

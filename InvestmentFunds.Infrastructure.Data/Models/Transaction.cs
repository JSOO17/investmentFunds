namespace InvestmentFunds.Domain.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid InvestmentFundId { get; set; }
        public DateTime Date { get; set; }
        public decimal AmountPayment { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}

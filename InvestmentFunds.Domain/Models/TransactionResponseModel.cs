namespace InvestmentFunds.Domain.Models
{
    public class TransactionResponseModel : TransactionModel
    {
        public InvestmentFundModel InvestmentFundDetails { get; set; }
    }
}

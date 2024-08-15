using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Infrastructure.Data.Models
{
    public class TransactionWithFund : Transaction
    {
        public InvestmentFund InvestmentFundDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentFunds.Domain.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public Guid InvestmentFundId { get; set; }
        public DateTime Date { get; set; }
        public decimal AmountPayment { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}

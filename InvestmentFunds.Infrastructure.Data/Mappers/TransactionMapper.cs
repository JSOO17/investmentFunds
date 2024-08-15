using AutoMapper;
using InvestmentFunds.Domain.Models;
using InvestmentFunds.Infrastructure.Data.Models;

namespace InvestmentFunds.Infrastructure.Data.Mappers
{
    public class TransactionMapper
    {
        private readonly IMapper _mapper;

        public TransactionMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Transaction ToTransaction(TransactionModel transactionModel)
        {
            return _mapper.Map<Transaction>(transactionModel);
        }

        public List<TransactionResponseModel> ToModel(List<TransactionWithFund> transactions)
        {
            return _mapper.Map<List<TransactionResponseModel>>(transactions);
        }
    }
}

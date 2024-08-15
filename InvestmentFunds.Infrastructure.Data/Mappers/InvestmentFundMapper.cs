using AutoMapper;
using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Infrastructure.Data.Mappers
{
    public class InvestmentFundMapper
    {
        private readonly IMapper _mapper;

        public InvestmentFundMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public InvestmentFundModel ToModel(InvestmentFund investmentFund)
        {
            return _mapper.Map<InvestmentFundModel>(investmentFund);
        }

        public List<InvestmentFundModel> ToModel(List<InvestmentFund> investmentFund)
        {
            return _mapper.Map<List<InvestmentFundModel>>(investmentFund);
        }
    }
}

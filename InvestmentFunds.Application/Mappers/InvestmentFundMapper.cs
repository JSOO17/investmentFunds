using AutoMapper;
using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Domain.Models;

namespace InvestmentFunds.Application.Mappers
{
    public class InvestmentFundMapper
    {
        private readonly IMapper _mapper;

        public InvestmentFundMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public InvestmentFundResponse ToResponse(InvestmentFundModel investmentFund)
        {
            return _mapper.Map<InvestmentFundResponse>(investmentFund);
        }

        public List<InvestmentFundResponse> ToResponse(List<InvestmentFundModel> investmentFunds)
        {
            return _mapper.Map<List<InvestmentFundResponse>>(investmentFunds);
        }
    }
}

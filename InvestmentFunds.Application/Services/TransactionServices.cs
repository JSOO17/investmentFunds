using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Application.Mappers;
using InvestmentFunds.Application.Services.Interfaces;
using InvestmentFunds.Domain.Interfaces.API;

namespace InvestmentFunds.Application.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionServicePort _servicePort;
        private readonly TransactionMapper _mapper;

        public TransactionServices(ITransactionServicePort servicePort, TransactionMapper mapper)
        {
            _servicePort = servicePort;
            _mapper = mapper;
        }

        public async Task<List<TransactionResponse>> GetAll()
        {
            var result = await _servicePort.GetAll();
            return _mapper.ToResponse(result);
        }
    }
}

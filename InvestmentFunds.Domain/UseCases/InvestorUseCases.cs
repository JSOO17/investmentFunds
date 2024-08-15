using InvestmentFunds.Domain.Interfaces.API;
using InvestmentFunds.Domain.Interfaces.SPI;

namespace InvestmentFunds.Domain.UseCases
{
    public class InvestorUseCases : IInvestorServicePort
    {
        private readonly IInvestorPersistencePort _port;

        public InvestorUseCases(IInvestorPersistencePort port)
        {
            _port = port;
        }

        public async Task<decimal> GetAmmountById(Guid id)
        {
            return await _port.GetAmmountById(id);
        }
    }
}

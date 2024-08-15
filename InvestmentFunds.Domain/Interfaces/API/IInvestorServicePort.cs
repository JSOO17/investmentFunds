namespace InvestmentFunds.Domain.Interfaces.API
{
    public interface IInvestorServicePort
    {
        Task<decimal> GetAmmountById(Guid id);
    }
}

namespace InvestmentFunds.Domain.Interfaces.SPI
{
    public interface IInvestorPersistencePort
    {
        Task Update(Guid id, decimal amount);
        Task<decimal> GetAmmountById(Guid id);
    }
}

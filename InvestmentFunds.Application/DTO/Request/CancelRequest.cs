namespace InvestmentFunds.Application.DTO.Request
{
    public class CancelRequest
    {
        public Guid Id { get; set; }
        public Guid InvestorId { get; set; }
    }
}

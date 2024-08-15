using InvestmentFunds.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentFunds.Application.Services.Interfaces
{
    public interface IInvestmentFundServices
    {
        Task<List<InvestmentFundResponse>> GetAll();
        Task<InvestmentFundResponse> GetById(Guid id);
    }
}

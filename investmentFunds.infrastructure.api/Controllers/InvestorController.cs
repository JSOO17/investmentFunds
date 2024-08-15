using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Application.Services.Interfaces;
using InvestmentFunds.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InvestmentFunds.Infrastructure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestorController : ControllerBase
    {
        private readonly IInvestorServices _services;
        private readonly ILogger<InvestorController> _logger;

        public InvestorController(IInvestorServices services, ILogger<InvestorController> logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvestorResponse>> Get(Guid id)
        {
            try
            {
                return Ok(await _services.GetAmmountById(id));
            }
            catch (ResourceNotFoundException ex)
            {
                var msg = $"Investor {id} was not found.";
                _logger.LogError(ex, msg);

                return StatusCode(((int)HttpStatusCode.BadRequest), msg);
            }
            catch (Exception ex)
            {
                var msg = "Something was wrong";
                _logger.LogError(ex, msg);

                return StatusCode(((int)HttpStatusCode.InternalServerError), msg);
            }
        }
    }
}

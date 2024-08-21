using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InvestmentFunds.Infrastructure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transactionServices;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ITransactionServices transactionServices, ILogger<TransactionController> logger)
        {
            _transactionServices = transactionServices;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<TransactionResponse>>> Get()
        {
            try
            {
                var result = await _transactionServices.GetAll();

                _logger.LogInformation($"Fetching {result.Count} transactions");

                return Ok(result);
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

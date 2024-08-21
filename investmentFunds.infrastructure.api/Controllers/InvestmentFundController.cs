using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Application.Services.Interfaces;
using InvestmentFunds.Domain.Exceptions;
using InvestmentFunds.Infrastructure.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InvestmentFunds.Infrastructure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentFundController : ControllerBase
    {
        private readonly IInvestmentFundServices _investmentFundService;
        private readonly ILogger<InvestmentFundController> _logger;

        public InvestmentFundController(IInvestmentFundServices investmentFundService, ILogger<InvestmentFundController> logger)
        {
            _investmentFundService = investmentFundService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvestmentFundResponse>>> Get()
        {
            try
            {
                var result = await _investmentFundService.GetAll();

                _logger.LogInformation($"Fetching {result.Count} investment funds");

                return Ok(result);
            }
            catch(Exception ex)
            {
                var msg = "Something was wrong";
                _logger.LogError(ex, msg);

                return StatusCode(((int)HttpStatusCode.InternalServerError), new ApiResponse
                {
                    Message = msg
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvestmentFundResponse>> Get(Guid id)
        {
            try
            {
                return Ok(await _investmentFundService.GetById(id));
            }
            catch(ResourceNotFoundException ex)
            {
                var msg = $"Investment found {id} was not found.";
                _logger.LogError(ex, msg);

                return StatusCode(((int)HttpStatusCode.NotFound), new ApiResponse
                {
                    Message = msg
                });
            }
            catch (Exception ex)
            {
                var msg = "Something was wrong";
                _logger.LogError(ex, msg);

                return StatusCode(((int)HttpStatusCode.InternalServerError), new ApiResponse
                {
                    Message = msg
                });
            }
        }
    }
}

using InvestmentFunds.Application.DTO.Request;
using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Application.Services.Interfaces;
using InvestmentFunds.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvestmentFunds.Infrastructure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionServices _subscriptionServices;
        private readonly ILogger<SubscriptionController> _logger;

        public SubscriptionController(ISubscriptionServices subscriptionServices, ILogger<SubscriptionController> logger)
        {
            _subscriptionServices = subscriptionServices;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubscriptionResponse>>> Get()
        {
            try
            {
                var result = await _subscriptionServices.GetAll();

                _logger.LogInformation($"Fetching {result.Count} subscriptions");

                return Ok(result);
            }
            catch (Exception ex)
            {
                var msg = "Something was wrong";
                _logger.LogError(ex, msg);

                return StatusCode(((int)HttpStatusCode.InternalServerError), msg);
            }
        }

        // POST api/<SubscriptionController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SubscriptionRequest request)
        {
            try
            {
                await _subscriptionServices.Subscribe(request);

                var msg = $"User {request.InvestorId} has been subscribed to {request.InvestmentFundId} InvestmentFund with a payment {request.AmountPayment}";
                _logger.LogInformation(msg);

                return StatusCode((int)HttpStatusCode.Created, msg);
            }
            catch (ResourceNotFoundException ex)
            {
                var msg = $"InvestmentFund {request.InvestmentFundId} was not found.";
                _logger.LogError(ex, msg);

                return StatusCode(((int)HttpStatusCode.NotFound), msg);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Something was wrong");

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                var msg = "Something was wrong";
                _logger.LogError(ex, msg);

                return StatusCode(((int)HttpStatusCode.InternalServerError), msg);
            }
        }

        // DELETE api/<SubscriptionController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromBody] CancelRequest request)
        {
            try
            {
                await _subscriptionServices.Cancel(request.Id, request.InvestorId);

                var msg = $"User {request.InvestorId} has been canceled to subscription {request.Id}.";

                _logger.LogInformation(msg);

                return Ok(msg);
            }
            catch (ResourceNotFoundException ex)
            {
                _logger.LogError(ex, "Something was wrong");

                return NotFound($"Subscription {request.Id} was not found.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Something was wrong");

                return StatusCode(((int)HttpStatusCode.BadRequest), ex.Message);
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

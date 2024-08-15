using InvestmentFunds.Domain.Exceptions;
using InvestmentFunds.Domain.Interfaces.API;
using InvestmentFunds.Domain.Interfaces.SPI;
using InvestmentFunds.Domain.Models;
using InvestmentFunds.Domain.Utils;

namespace InvestmentFunds.Domain.UseCases
{
    public class SubscriptionUseCases : ISubscriptionServicePort
    {
        private readonly IInvestorPersistencePort _investorPersistence;
        private readonly ISubscriptionPersistencePort _subscriptionPersistence;
        private readonly IInvestmentFundPersistencePort _investmentFundPersistence;
        private readonly ITransactionPersistencePort _transactionPersistence;

        public SubscriptionUseCases(IInvestorPersistencePort investorPersistence, ISubscriptionPersistencePort subscriptionPersistence, IInvestmentFundPersistencePort investmentFundPersistence, ITransactionPersistencePort transactionPersistence)
        {
            _investorPersistence = investorPersistence;
            _subscriptionPersistence = subscriptionPersistence;
            _investmentFundPersistence = investmentFundPersistence;
            _transactionPersistence = transactionPersistence;
        }

        public async Task Subscribe(SubscriptionModel model)
        {
            var getInvestorAmountTask = _investorPersistence.GetAmmountById(model.InvestorId);
            var getInvestmentFundTask = _investmentFundPersistence.GetById(model.InvestmentFundId) ?? throw new ResourceNotFoundException();

            await Task.WhenAll(getInvestorAmountTask, getInvestmentFundTask);

            var investorAmount = await getInvestorAmountTask;
            var investmentFund = await getInvestmentFundTask;

            ValidateAmount(model, investorAmount, investmentFund);

            await _subscriptionPersistence.Create(model);
            await _investorPersistence.Update(model.InvestorId, investorAmount - model.AmountPayment);
            await _transactionPersistence.Create(new TransactionModel
            {
                AmountPayment = model.AmountPayment,
                Date = DateTime.UtcNow,
                InvestmentFundId = model.InvestmentFundId,
                Type = TransactionType.Subscription
            });
            await _investmentFundPersistence.UpdateState(investmentFund.Id, InvestmentFundStates.Subscribed);
        }

        private static void ValidateAmount(SubscriptionModel model, decimal investorAmount, InvestmentFundModel investmentFund)
        {
            if (investorAmount < model.AmountPayment)
            {
                throw new InvalidOperationException($"You can't subscribe to {investmentFund.Name}. Investor amount is less than the amount payment.");
            }

            if (model.AmountPayment < investmentFund.MinimumPayment)
            {
                throw new InvalidOperationException($"You can't subscribe to {investmentFund.Name}. AmountPayment is less than the minimum payment required.");
            }
        }

        public async Task Cancel(Guid id, Guid investorId)
        {
            var investorAmount = await _investorPersistence.GetAmmountById(investorId);
            var subscription = await _subscriptionPersistence.GetById(id) ?? throw new ResourceNotFoundException();

            await _subscriptionPersistence.Delete(id);
            var currentInvestorAmount = investorAmount + subscription.AmountPayment;
            await _investorPersistence.Update(investorId, currentInvestorAmount);
            await _transactionPersistence.Create(new TransactionModel
            {
                AmountPayment = subscription.AmountPayment,
                Date = DateTime.UtcNow,
                InvestmentFundId = subscription.InvestmentFundId,
                Type = TransactionType.Cancelation
            });
            await _investmentFundPersistence.UpdateState(subscription.InvestmentFundId, InvestmentFundStates.Open);
        }

        public async Task<List<SubscriptionResponseModel>> GetAll() => await _subscriptionPersistence.GetAll();
    }
}

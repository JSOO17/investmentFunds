using InvestmentFunds.Domain.Models;
using InvestmentFunds.Domain.Utils;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentFunds.Infrastructure.Data.Initializers
{
    public class MongoInitializer
    {
        public static async Task InitializeAsync(IMongoDatabase database)
        {
            await SeedInvestmentFunds(database);
            await SeedInvestor(database);
        }

        private static async Task SeedInvestmentFunds(IMongoDatabase database)
        {
            var investmentFundCollection = database.GetCollection<InvestmentFund>("investmentFund");

            var result = investmentFundCollection.Find(_ => true).ToList();

            if ((await investmentFundCollection.CountDocumentsAsync(_ => true)) == 0)
            {
                var investmnetFunds = new[]
                {
                    new InvestmentFund {
                        Id = Guid.NewGuid(),
                        Name = "FPV_BTG_PACTUAL_RECAUDADORA",
                        MinimumPayment = 75000,
                        Category = CategoryInvestmentFund.FPV
                    },
                    new InvestmentFund {
                        Id = Guid.NewGuid(),
                        Name = "FPV_BTG_PACTUAL_ECOPETROL",
                        MinimumPayment = 125000,
                        Category = CategoryInvestmentFund.FPV
                    },
                    new InvestmentFund {
                        Id = Guid.NewGuid(),
                        Name = "DEUDAPRIVADA",
                        MinimumPayment = 50000,
                        Category = CategoryInvestmentFund.FIC
                    },
                    new InvestmentFund {
                        Id = Guid.NewGuid(),
                        Name = "FDO-ACCIONES",
                        MinimumPayment = 250000,
                        Category = CategoryInvestmentFund.FIC
                    },
                    new InvestmentFund {
                        Id = Guid.NewGuid(),
                        Name = "FPV_BTG_PACTUAL_DINAMICA",
                        MinimumPayment = 100000,
                        Category = CategoryInvestmentFund.FPV
                    }
                };

                await investmentFundCollection.InsertManyAsync(investmnetFunds);
            }
        }

        private static async Task SeedInvestor(IMongoDatabase database)
        {
            var investorCollection = database.GetCollection<Investor>("investor");

            if ((await investorCollection.CountDocumentsAsync(_ => true)) == 0)
            {
                await investorCollection.InsertOneAsync(new Investor
                {
                    Id = Guid.NewGuid(),
                    Amount = 500000
                });
            }
        }
    }
}

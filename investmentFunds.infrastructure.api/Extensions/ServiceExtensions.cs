using AutoMapper;
using InvestmentFunds.Application.Services.Interfaces;
using InvestmentFunds.Application.Services;
using InvestmentFunds.Domain.Interfaces.API;
using InvestmentFunds.Domain.Interfaces.SPI;
using InvestmentFunds.Domain.Models;
using InvestmentFunds.Domain.UseCases;
using InvestmentFunds.Infrastructure.Data.Initializers;
using InvestmentFunds.Infrastructure.Data.Repositories;
using MongoDB.Driver;
using InvestmentFunds.Infrastructure.Data.Models;
using InvestmentFunds.Application.DTO.Response;
using InvestmentFunds.Application.DTO.Request;

namespace InvestmentFunds.Infrastructure.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("MongoDbConnectionString");
            Console.WriteLine($"MongoDB Connection String: {connectionString}");
            var mongoClient = new MongoClient(connectionString);
            var databaseName = configuration.GetSection("DatabaseSettings:DatabaseName").Value;
            var mongoDatabase = mongoClient.GetDatabase(databaseName);

            services.AddSingleton(mongoDatabase);

            // Read collection names from configuration
            var collectionNames = configuration.GetSection("DatabaseSettings:Collections").Get<Dictionary<string, string>>();

            services.AddScoped(provider =>
            {
                var db = provider.GetRequiredService<IMongoDatabase>();
                return db.GetCollection<InvestmentFund>(collectionNames["InvestmentFund"]);
            });

            services.AddScoped(provider =>
            {
                var db = provider.GetRequiredService<IMongoDatabase>();
                return db.GetCollection<Investor>(collectionNames["Investor"]);
            });

            services.AddScoped(provider =>
            {
                var db = provider.GetRequiredService<IMongoDatabase>();
                return db.GetCollection<Subscription>(collectionNames["Subscription"]);
            });

            services.AddScoped(provider =>
            {
                var db = provider.GetRequiredService<IMongoDatabase>();
                return db.GetCollection<Transaction>(collectionNames["Transaction"]);
            });
        }

        public static void ConfigureMappings(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.CreateMap<SubscriptionWithFund, SubscriptionResponseModel>();
                mc.CreateMap<SubscriptionModel, Subscription>();
                mc.CreateMap<Subscription, SubscriptionModel>();
                mc.CreateMap<InvestmentFundModel, InvestmentFund>();
                mc.CreateMap<InvestmentFund, InvestmentFundModel>();
                mc.CreateMap<TransactionWithFund, TransactionResponseModel>();
                mc.CreateMap<TransactionResponseModel, TransactionResponse>();
                mc.CreateMap<TransactionModel, Transaction>();
                mc.CreateMap<Transaction, TransactionModel>();
                mc.CreateMap<InvestmentFundModel, InvestmentFundResponse>();
                mc.CreateMap<SubscriptionResponseModel, SubscriptionResponse>();
                mc.CreateMap<SubscriptionRequest, SubscriptionModel>();
            }).CreateMapper());
        }

        public static void ConfigureMappers(this IServiceCollection services)
        {
            services.AddSingleton<InvestmentFunds.Application.Mappers.InvestmentFundMapper>();
            services.AddSingleton<InvestmentFunds.Infrastructure.Data.Mappers.InvestmentFundMapper>();

            services.AddSingleton<InvestmentFunds.Application.Mappers.SubscriptionMapper>();
            services.AddSingleton<InvestmentFunds.Infrastructure.Data.Mappers.SubscriptionMapper>();

            services.AddSingleton<InvestmentFunds.Application.Mappers.TransactionMapper>();
            services.AddSingleton<InvestmentFunds.Infrastructure.Data.Mappers.TransactionMapper>();

        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<IInvestmentFundPersistencePort, InvestmentFundRepository>();
            services.AddTransient<IInvestorPersistencePort, InvestorRepository>();
            services.AddTransient<ITransactionPersistencePort, TransactionRepository>();
            services.AddTransient<ISubscriptionPersistencePort, SubscriptionRepository>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IInvestmentFundServicePort, InvestmentFundUseCases>();
            services.AddTransient<IInvestorServicePort, InvestorUseCases>();
            services.AddTransient<ITransactionServicePort, TransactionUseCases>();
            services.AddTransient<ISubscriptionServicePort, SubscriptionUseCases>();

            services.AddTransient<IInvestmentFundServices, InvestmentFundServices>();
            services.AddTransient<IInvestorServices, InvestorServices>();
            services.AddTransient<ITransactionServices, TransactionServices>();
            services.AddTransient<ISubscriptionServices, SubscriptionServices>();
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
        }

        public static async Task InitializeDatabaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
            await MongoInitializer.InitializeAsync(db);
        }
    }
}

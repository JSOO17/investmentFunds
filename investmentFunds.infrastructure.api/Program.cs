using InvestmentFunds.Infrastructure.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureMappers();
builder.Services.ConfigureMongoDb(builder.Configuration);
builder.Services.ConfigureMappings();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureCors();

var app = builder.Build();

await app.InitializeDatabaseAsync();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();

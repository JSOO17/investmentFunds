#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["investmentFunds.infrastructure.api/InvestmentFunds.Infrastructure.Api.csproj", "investmentFunds.infrastructure.api/"]
COPY ["InvestmentFunds.Application/InvestmentFunds.Application.csproj", "InvestmentFunds.Application/"]
COPY ["InvestmentFunds.Domain/InvestmentFunds.Domain.csproj", "InvestmentFunds.Domain/"]
COPY ["InvestmentFunds.Infrastructure.Data/InvestmentFunds.Infrastructure.Data.csproj", "InvestmentFunds.Infrastructure.Data/"]
RUN dotnet restore "investmentFunds.infrastructure.api/InvestmentFunds.Infrastructure.Api.csproj"
COPY . .
WORKDIR "/src/investmentFunds.infrastructure.api"
RUN dotnet build "InvestmentFunds.Infrastructure.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvestmentFunds.Infrastructure.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvestmentFunds.Infrastructure.Api.dll"]
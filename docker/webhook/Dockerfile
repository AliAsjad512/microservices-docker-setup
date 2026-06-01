
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY src/Webhooks.API/Webhooks.API.csproj src/Webhooks.API/
COPY Directory.Packages.props .
COPY Directory.Build.props .
COPY src/EventBusRabbitMQ/EventBusRabbitMQ.csproj src/EventBusRabbitMQ/
COPY src/IntegrationEventLogEF/IntegrationEventLogEF.csproj src/IntegrationEventLogEF/
COPY src/eShop.ServiceDefaults/eShop.ServiceDefaults.csproj src/eShop.ServiceDefaults/

RUN dotnet restore src/Webhooks.API/Webhooks.API.csproj

COPY . .

RUN dotnet publish src/Webhooks.API/Webhooks.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runner

ENV ASPNETCORE_HTTP_PORTS=5227
EXPOSE 5227
WORKDIR /app
COPY --from=build /app/publish .
CMD ["dotnet","Webhooks.API.dll"]

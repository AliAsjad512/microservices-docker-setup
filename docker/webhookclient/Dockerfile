
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY src/WebhookClient/WebhookClient.csproj src/WebhookClient/
COPY Directory.Packages.props .
COPY Directory.Build.props .
COPY src/EventBusRabbitMQ/EventBusRabbitMQ.csproj src/EventBusRabbitMQ/
COPY src/IntegrationEventLogEF/IntegrationEventLogEF.csproj src/IntegrationEventLogEF/
COPY src/eShop.ServiceDefaults/eShop.ServiceDefaults.csproj src/eShop.ServiceDefaults/

RUN dotnet restore src/WebhookClient/WebhookClient.csproj

COPY . .

RUN dotnet publish src/WebhookClient/WebhookClient.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runner

ENV ASPNETCORE_HTTP_PORTS=5062
EXPOSE 5062
WORKDIR /app
COPY --from=build /app/publish .
CMD ["dotnet","WebhookClient.dll"]

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY src/OrderProcessor/OrderProcessor.csproj src/OrderProcessor/
COPY Directory.Packages.props .
COPY Directory.Build.props .
COPY src/EventBusRabbitMQ/EventBusRabbitMQ.csproj src/EventBusRabbitMQ/
COPY src/IntegrationEventLogEF/IntegrationEventLogEF.csproj src/IntegrationEventLogEF/
COPY src/eShop.ServiceDefaults/eShop.ServiceDefaults.csproj src/eShop.ServiceDefaults/

RUN dotnet restore src/OrderProcessor/OrderProcessor.csproj

COPY . .

RUN dotnet publish src/OrderProcessor/OrderProcessor.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runner

ENV ASPNETCORE_HTTP_PORTS=16888
EXPOSE 16888
WORKDIR /app
COPY --from=build /app/publish .
CMD ["dotnet","OrderProcessor.dll"]

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY src/Ordering.API/Ordering.API.csproj src/Ordering.API/
COPY Directory.Packages.props .
COPY Directory.Build.props .
COPY src/EventBusRabbitMQ/EventBusRabbitMQ.csproj src/EventBusRabbitMQ/
COPY src/IntegrationEventLogEF/IntegrationEventLogEF.csproj src/IntegrationEventLogEF/
COPY src/eShop.ServiceDefaults/eShop.ServiceDefaults.csproj src/eShop.ServiceDefaults/

RUN dotnet restore src/Ordering.API/Ordering.API.csproj

COPY . .

RUN dotnet publish src/Ordering.API/Ordering.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runner

ENV ASPNETCORE_HTTP_PORTS=5224
EXPOSE 5224
WORKDIR /app
COPY --from=build /app/publish .
CMD ["dotnet","Ordering.API.dll"]

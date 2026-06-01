FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# WORKDIR should be a DIRECTORY not a file path
WORKDIR /src

# Make COPY destination consistent with other paths
COPY src/Catalog.API/Catalog.API.csproj src/Catalog.API/
COPY Directory.Packages.props .
COPY Directory.Build.props .
COPY src/EventBusRabbitMQ/EventBusRabbitMQ.csproj src/EventBusRabbitMQ/
COPY src/IntegrationEventLogEF/IntegrationEventLogEF.csproj src/IntegrationEventLogEF/
COPY src/eShop.ServiceDefaults/eShop.ServiceDefaults.csproj src/eShop.ServiceDefaults/

RUN dotnet restore src/Catalog.API/Catalog.API.csproj

COPY . .

RUN dotnet publish src/Catalog.API/Catalog.API.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runner

ENV ASPNETCORE_HTTP_PORTS=5222
EXPOSE 5222
WORKDIR /app
COPY --from=build /app/publish .
CMD ["dotnet", "Catalog.API.dll"]

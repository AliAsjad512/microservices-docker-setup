FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY src/WebApp/WebApp.csproj src/WebApp/
COPY Directory.Packages.props .
COPY Directory.Build.props .
COPY src/EventBusRabbitMQ/EventBusRabbitMQ.csproj src/EventBusRabbitMQ/
COPY src/IntegrationEventLogEF/IntegrationEventLogEF.csproj src/IntegrationEventLogEF/
COPY src/eShop.ServiceDefaults/eShop.ServiceDefaults.csproj src/eShop.ServiceDefaults/

RUN dotnet restore src/WebApp/WebApp.csproj

COPY . .

RUN dotnet publish src/WebApp/WebApp.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runner

ENV ASPNETCORE_HTTP_PORTS=5045
EXPOSE 5045
WORKDIR /app
COPY --from=build /app/publish .
CMD ["dotnet","WebApp.dll"]

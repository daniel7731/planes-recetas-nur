FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["PlanesRecetas.application/PlanesRecetas.application.csproj", "PlanesRecetas.application/"]
COPY ["PlanesRecetas.domain/PlanesRecetas.domain.csproj", "PlanesRecetas.domain/"]
COPY ["PlanesRecetas.infraestructure/PlanesRecetas.infraestructure.csproj", "PlanesRecetas.infraestructure/"]
COPY ["PlanesRecetasWorkerService/PlanesRecetasWorkerService.csproj","PlanesRecetasWorkerService/"] 

RUN dotnet restore "PlanesRecetasWorkerService/PlanesRecetasWorkerService.csproj"

COPY . .

WORKDIR "/src/PlanesRecetasWorkerService"

RUN dotnet build "PlanesRecetasWorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PlanesRecetasWorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DataBaseSettings__ConnectionString=Server=sqlserver;Database=appservice;User Id=sa;Password=Password12345;TrustServerCertificate=True;

ENTRYPOINT ["dotnet", "PlanesRecetasWorkerService.dll"]

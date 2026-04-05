FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
# Set these early so the runtime respects them immediately
ENV ASPNETCORE_ENVIRONMENT=Development
# Use a single backslash or forward slash for Linux compatibility
ENV DataBaseSettings__ConnectionString="Host=planes-postgres;Port=5432;Database=appservice;Username=postgres;Password=Password12345"
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
ENTRYPOINT ["dotnet", "PlanesRecetasWorkerService.dll"]

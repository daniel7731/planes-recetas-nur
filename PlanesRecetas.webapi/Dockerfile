FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 17080
EXPOSE 17081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Copy ALL project files first (including the core dependency)
COPY ["PlanesRecetas.webapi/PlanesRecetas.webapi.csproj", "PlanesRecetas.webapi/"]
COPY ["PlanesRecetas.application/PlanesRecetas.application.csproj", "PlanesRecetas.application/"]
COPY ["PlanesRecetas.domain/PlanesRecetas.domain.csproj", "PlanesRecetas.domain/"]
COPY ["PlanesRecetas.infraestructure/PlanesRecetas.infraestructure.csproj", "PlanesRecetas.infraestructure/"]
COPY ["PlanesRecetas.testing/PlanesRecetas.testing.csproj", "PlanesRecetas.testing/"]

# 2. Restore using the WebApi as the entry point
RUN dotnet restore "PlanesRecetas.webapi/PlanesRecetas.webapi.csproj"

# 3. Copy the entire source tree
COPY . .

# 4. Build and Publish
WORKDIR "/src/PlanesRecetas.webapi"
RUN dotnet build "PlanesRecetas.webapi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PlanesRecetas.webapi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DataBaseSettings__ConnectionString="Server=planes-sqlserver;Database=appservice;User Id=sa;Password=Password12345;TrustServerCertificate=True;"

ENTRYPOINT ["dotnet", "PlanesRecetas.webapi.dll"]
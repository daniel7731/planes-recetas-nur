FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Copy ALL project files first (including the core dependency)
COPY ["PlanesRecetas.webapi/PlanesRecetas.webapi.csproj", "PlanesRecetas.webapi/"]
COPY ["PlanesRecetas.application/PlanesRecetas.application.csproj", "PlanesRecetas.application/"]
COPY ["PlanesRecetas.domain/PlanesRecetas.domain.csproj", "PlanesRecetas.domain/"]
COPY ["PlanesRecetas.infraestructure/PlanesRecetas.infraestructure.csproj", "PlanesRecetas.infraestructure/"]
COPY ["PlanesRecetas.testing/PlanesRecetas.testing.csproj", "PlanesRecetas.testing/"]
COPY ["joseco.ddd.core/joseco.ddd.core.csproj", "joseco.ddd.core/"]

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
ENTRYPOINT ["dotnet", "PlanesRecetas.webapi.dll"]
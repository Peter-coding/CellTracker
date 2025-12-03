# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
# oficcially it was runtime instead of aspnet
FROM mcr.microsoft.com/dotnet/aspnet:9.0-noble-chiseled-extra AS base
USER $APP_UID
WORKDIR /app


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
RUN echo "PWD:" && pwd && echo "FILES:" && ls -la && echo "TREE:" && find . -maxdepth 4 -type f
COPY ["CellTracker.MqttIngestion.csproj", "."]
RUN dotnet restore "./CellTracker.MqttIngestion.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./CellTracker.MqttIngestion.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CellTracker.MqttIngestion.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CellTracker.MqttIngestion.dll"]
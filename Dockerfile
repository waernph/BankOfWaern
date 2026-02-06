# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM node:22-alpine
USER $APP_UID
WORKDIR /app
EXPOSE 3000


# This stage is used to build the service project
FROM node:22-alpine AS base
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bank of Waern.csproj", "."]
RUN dotnet restore "./Bank of Waern.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Bank of Waern.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Bank of Waern.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bank of Waern.dll"]
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/server/AppGateways/WebApiGateway/Web.ApiGateway/Web.ApiGateway.csproj", "src/server/AppGateways/WebApiGateway/Web.ApiGateway/"]
RUN dotnet restore "./src/server/AppGateways/WebApiGateway/Web.ApiGateway/Web.ApiGateway.csproj"
COPY . .
WORKDIR "/src/src/server/AppGateways/WebApiGateway/Web.ApiGateway"
RUN dotnet build "./Web.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Web.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.ApiGateway.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/PageService/PageService/PageService.csproj", "src/Services/PageService/PageService/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.Factory/EventBus.Factory.csproj", "src/BuildingBlocks/EventBus/EventBus.Factory/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.Base/EventBus.Base.csproj", "src/BuildingBlocks/EventBus/EventBus.Base/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "src/BuildingBlocks/EventBus/EventBus.RabbitMQ/"]
COPY ["src/BuildingBlocks/IdentityEx/IdentityEx/IdentityEx.csproj", "src/BuildingBlocks/IdentityEx/IdentityEx/"]
COPY ["src/Services/PageService/Domain/Domain.csproj", "src/Services/PageService/Domain/"]
COPY ["src/Services/PageService/Application/Application.csproj", "src/Services/PageService/Application/"]
COPY ["src/BuildingBlocks/Consul/ConsulConfig/ConsulConfig.csproj", "src/BuildingBlocks/Consul/ConsulConfig/"]
COPY ["src/BuildingBlocks/SwaggerEx/SwaggerEx/SwaggerEx.csproj", "src/BuildingBlocks/SwaggerEx/SwaggerEx/"]
COPY ["src/Services/PageService/Insfrastructure/Insfrastructure.csproj", "src/Services/PageService/Insfrastructure/"]
RUN dotnet restore "src/Services/PageService/PageService/PageService.csproj"
COPY . .
WORKDIR "/src/src/Services/PageService/PageService"
RUN dotnet build "PageService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PageService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PageService.dll"]
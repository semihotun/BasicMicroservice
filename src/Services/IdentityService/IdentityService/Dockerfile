#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/IdentityService/IdentityService/IdentityService.csproj", "src/Services/IdentityService/IdentityService/"]
COPY ["src/BuildingBlocks/Consul/ConsulConfig/ConsulConfig.csproj", "src/BuildingBlocks/Consul/ConsulConfig/"]
RUN dotnet restore "src/Services/IdentityService/IdentityService/IdentityService.csproj"
COPY . .
WORKDIR "/src/src/Services/IdentityService/IdentityService"
RUN dotnet build "IdentityService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.dll"]

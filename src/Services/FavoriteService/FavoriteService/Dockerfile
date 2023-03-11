#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/FavoriteService/FavoriteService/FavoriteService.csproj", "src/Services/FavoriteService/FavoriteService/"]
COPY ["src/BuildingBlocks/IdentityEx/IdentityEx/IdentityEx.csproj", "src/BuildingBlocks/IdentityEx/IdentityEx/"]
COPY ["src/BuildingBlocks/Consul/ConsulConfig/ConsulConfig.csproj", "src/BuildingBlocks/Consul/ConsulConfig/"]
COPY ["src/BuildingBlocks/SwaggerEx/SwaggerEx/SwaggerEx.csproj", "src/BuildingBlocks/SwaggerEx/SwaggerEx/"]
RUN dotnet restore "src/Services/FavoriteService/FavoriteService/FavoriteService.csproj"
COPY . .
WORKDIR "/src/src/Services/FavoriteService/FavoriteService"
RUN dotnet build "FavoriteService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FavoriteService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoriteService.dll"]
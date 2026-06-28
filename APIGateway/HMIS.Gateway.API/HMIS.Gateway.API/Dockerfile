#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APIGateway/HMIS.Gateway.API/HMIS.Gateway.API/HMIS.Gateway.API.csproj", "APIGateway/HMIS.Gateway.API/HMIS.Gateway.API/"]
RUN dotnet restore "APIGateway/HMIS.Gateway.API/HMIS.Gateway.API/HMIS.Gateway.API.csproj"
COPY . .
WORKDIR "/src/APIGateway/HMIS.Gateway.API/HMIS.Gateway.API"
RUN dotnet build "HMIS.Gateway.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HMIS.Gateway.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HMIS.Gateway.API.dll"]
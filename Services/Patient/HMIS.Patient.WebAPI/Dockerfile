#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Patient/HMIS.Patient.WebAPI/HMIS.Patient.WebAPI.csproj", "Services/Patient/HMIS.Patient.WebAPI/"]
RUN dotnet restore "Services/Patient/HMIS.Patient.WebAPI/HMIS.Patient.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/Patient/HMIS.Patient.WebAPI"
RUN dotnet build "HMIS.Patient.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HMIS.Patient.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HMIS.Patient.WebAPI.dll"]
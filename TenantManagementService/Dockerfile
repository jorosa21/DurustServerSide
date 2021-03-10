#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TenantManagementService/TenantManagementService.csproj", "TenantManagementService/"]
RUN dotnet restore "TenantManagementService/TenantManagementService.csproj"
COPY . .
WORKDIR "/src/TenantManagementService"
RUN dotnet build "TenantManagementService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TenantManagementService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TenantManagementService.dll"]
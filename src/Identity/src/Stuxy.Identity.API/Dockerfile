#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Stuxy.Identity.API/Stuxy.Identity.API.csproj", "src/Stuxy.Identity.API/"]
COPY ["src/Stuxy.Identity.Abstractions/Stuxy.Identity.Abstractions.csproj", "src/Stuxy.Identity.Abstractions/"]
COPY ["src/Stuxy.Identity.Contracts/Stuxy.Identity.Contracts.csproj", "src/Stuxy.Identity.Contracts/"]
COPY ["src/Stuxy.Identity.Infrastructure/Stuxy.Identity.Infrastructure.csproj", "src/Stuxy.Identity.Infrastructure/"]
COPY ["src/Stuxy.Identity.Core/Stuxy.Identity.Core.csproj", "src/Stuxy.Identity.Core/"]
COPY ["nuget.config", ""]
RUN dotnet restore --configfile "nuget.config" "src/Stuxy.Identity.API/Stuxy.Identity.API.csproj"
COPY . .
WORKDIR "/src/src/Stuxy.Identity.API"
RUN dotnet build "Stuxy.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Stuxy.Identity.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stuxy.Identity.API.dll"]
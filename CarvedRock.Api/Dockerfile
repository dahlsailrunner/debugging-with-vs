#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CarvedRock.Api/CarvedRock.Api.csproj", "CarvedRock.Api/"]
COPY ["CarvedRock.Domain/CarvedRock.Domain.csproj", "CarvedRock.Domain/"]
COPY ["CarvedRock.Core/CarvedRock.Core.csproj", "CarvedRock.Core/"]
COPY ["CarvedRock.Data/CarvedRock.Data.csproj", "CarvedRock.Data/"]
RUN dotnet restore "CarvedRock.Api/CarvedRock.Api.csproj"
COPY . .
WORKDIR "/src/CarvedRock.Api"
RUN dotnet build "CarvedRock.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarvedRock.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarvedRock.Api.dll"]
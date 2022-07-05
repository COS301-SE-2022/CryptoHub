FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BusinessLogic/BusinessLogic.csproj","./BusinessLogic/"]
COPY ["CryptoHubAPI/CryptoHubAPI.csproj","./CryptoHubAPI/"]
COPY ["Domain/Domain.csproj","./Domain/"]
COPY ["Infrastructure/Infrastructure.csproj","./Infrastructure/"]
RUN dotnet restore "CryptoHubAPI/CryptoHubAPI.csproj"
COPY . .
WORKDIR "/src/CryptoHubAPI"
RUN dotnet build "CryptoHubAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CryptoHubAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptoHubAPI.dll"]

#GET SDK Image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app 
#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY ["BusinessLogic/BusinessLogic.csproj","./BusinessLogic/"]
COPY ["CryptoHubAPI/CryptoHubAPI.csproj","./CryptoHubAPI/"]
COPY ["Domain/Domain.csproj","./Domain/"]
COPY ["Infrastructure/Infrastructure.csproj","./Infrastructure/"]
COPY ["UnitTests/UnitTests.csproj","./UnitTests/"]

#
RUN dotnet restore 
#
# copy everything else and build app
COPY ["BusinessLogic/.","./BusinessLogic/"]
COPY ["CryptoHubAPI/.","./CryptoHubAPI/"]
COPY ["Domain/.","./Domain/"]
COPY ["Infrastructure/.","./Infrastructure/"]
COPY ["UnitTests/.","./UnitTests/"]
#
WORKDIR /app/CryptoHubAPI
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime
WORKDIR /app 
#
COPY --from=build /app/CryptoHubAPI/out ./
ENTRYPOINT ["dotnet", "CryptoHubAPI.dll"]
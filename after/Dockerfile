FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY /BookingMgmt.CoreWCF.WebService/*.csproj ./BookingMgmt.CoreWCF.WebService/
COPY /BookingMgmt.Application/*.csproj ./BookingMgmt.Application/
COPY /BookingMgmt.Contracts/*.csproj ./BookingMgmt.Contracts/
COPY /BookingMgmt.Domain/*.csproj ./BookingMgmt.Domain/
COPY /BookingMgmt.Infrastructure/*.csproj ./BookingMgmt.Infrastructure/
COPY /BookingMgmt.SharedKernel/*.csproj ./BookingMgmt.SharedKernel/
RUN dotnet restore "./BookingMgmt.CoreWCF.WebService/BookingMgmt.CoreWCF.WebService.csproj" -s "https://api.nuget.org/v3/index.json"

# Copy everything else and build
COPY . ./
RUN dotnet publish BookingMgmt.CoreWCF.WebService/*.csproj -c Release -o /app/publish

# Build runtime imagedock
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "BookingMgmt.CoreWCF.WebService.dll"]
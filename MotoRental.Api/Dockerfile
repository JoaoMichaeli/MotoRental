# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY MotoRental.sln ./
COPY MotoRental.Domain/*.csproj MotoRental.Domain/
COPY MotoRental.Application/*.csproj MotoRental.Application/
COPY MotoRental.Infrastructure/*.csproj MotoRental.Infrastructure/
COPY MotoRental.Api/*.csproj MotoRental.Api/
COPY MotoRental.Common/*.csproj MotoRental.Common/

RUN dotnet restore MotoRental.Api/MotoRental.Api.csproj

COPY . .
RUN dotnet publish MotoRental.Api/MotoRental.Api.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "MotoRental.Api.dll"]

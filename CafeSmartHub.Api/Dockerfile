# ---------- BUILD ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore CafeSmartHub.Api.csproj
RUN dotnet publish CafeSmartHub.Api.csproj -c Release -o /app/publish

# ---------- RUNTIME ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["sh","-c","ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080} dotnet CafeSmartHub.Api.dll"]

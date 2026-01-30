# ---- build ----
FROM mcr.microsoft.com / dotnet / sdk:8.0 AS build
WORKDIR /src

# Copiamos csproj y restauramos
COPY CafeSmartHub.Api.csproj ./
RUN dotnet restore

# Copiamos el resto y publicamos
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# ---- run ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Render suele exponer el puerto 10000; si no, se ajusta con PORT
ENV ASPNETCORE_URLS=http://0.0.0.0:10000

COPY--from = build / app / publish./
ENTRYPOINT["dotnet", "CafeSmartHub.Api.dll"]

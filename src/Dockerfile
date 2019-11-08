# FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
# WORKDIR /app


# FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
# WORKDIR /src
# COPY ["web.csproj", "./"]
# RUN dotnet restore "./web.csproj"
# COPY . .
# WORKDIR "/src/."
# RUN dotnet build "web.csproj" -c Release -o /app

# FROM build AS publish
# RUN dotnet publish "web.csproj" -c Release -o /app

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app .
# ENTRYPOINT ["dotnet", "web.dll"]


FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS builder

WORKDIR /web
COPY web/web.csproj .
RUN dotnet restore

COPY web/ .
RUN dotnet publish -c Release -o /out web.csproj

# app image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim-arm32v7

WORKDIR /app
ENTRYPOINT ["dotnet", "web.dll"]

COPY --from=builder /out/ .
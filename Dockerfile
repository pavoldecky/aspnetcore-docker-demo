FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["web.csproj", "./"]
RUN dotnet restore "./web.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "web.csproj" -c Release -o /app

FROM base AS final
COPY qemu-arm-static /usr/bin/qemu-arm-static
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "web.dll"]

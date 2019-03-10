FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["Client.MQTT.csproj", "./"]
RUN dotnet restore "./Client.MQTT.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Client.MQTT.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Client.MQTT.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Client.MQTT.dll"]

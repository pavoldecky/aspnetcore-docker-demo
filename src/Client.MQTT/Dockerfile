FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
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

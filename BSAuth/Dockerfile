FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CyberStrike/CyberStrike.csproj", "CyberStrike/"]
RUN dotnet restore "CyberStrike/CyberStrike.csproj"
COPY . .
WORKDIR "/src/CyberStrike"
RUN dotnet build "CyberStrike.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CyberStrike.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CyberStrike.dll"]

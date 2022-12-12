# https://hub.docker.com/_/microsoft-dotnet-core

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src

COPY ["src/DesafioATS.Domain.Core/", "DesafioATS.Domain.Core/"]
COPY ["src/DesafioATS.Domain/", "DesafioATS.Domain/"]
COPY ["src/DesafioATS.Data/", "DesafioATS.Data/"]
COPY ["src/DesafioATS.EventSources/", "DesafioATS.EventSources/"]
COPY ["src/DesafioATS.WebAPI/", "DesafioATS.WebAPI/"]

RUN dotnet restore "DesafioATS.WebAPI/DesafioATS.WebAPI.csproj"

WORKDIR /src/DesafioATS.WebAPI

RUN dotnet build "DesafioATS.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish DesafioATS.WebAPI.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "DesafioATS.WebAPI.dll"]
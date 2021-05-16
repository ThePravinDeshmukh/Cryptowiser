#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

FROM node:lts-buster-slim AS node_base
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY --from=node_base . .

WORKDIR /src
RUN npm -v
RUN dir
COPY ["Cryptowiser/Cryptowiser.csproj", "Cryptowiser/"]
COPY ["Cryptowiser.BusinessLogic/Cryptowiser.BusinessLogic.csproj", "Cryptowiser.BusinessLogic/"]
COPY ["Cryptowiser.Models/Cryptowiser.Models.csproj", "Cryptowiser.Models/"]
COPY ["Cryptowiser.ExternalServices/Cryptowiser.ExternalServices.csproj", "Cryptowiser.ExternalServices/"]
RUN dir
RUN dotnet restore "Cryptowiser/Cryptowiser.csproj"
COPY . .
WORKDIR "/src/Cryptowiser"
RUN dotnet build "Cryptowiser.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cryptowiser.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cryptowiser.dll"]
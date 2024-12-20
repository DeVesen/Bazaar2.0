#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DeVesen.Bazaar/Server/DeVesen.Bazaar.Server.csproj", "DeVesen.Bazaar/Server/"]
COPY ["DeVesen.Bazaar/Client/DeVesen.Bazaar.Client.csproj", "DeVesen.Bazaar/Client/"]
COPY ["DeVesen.Bazaar/Shared/DeVesen.Bazaar.Shared.csproj", "DeVesen.Bazaar/Shared/"]
RUN dotnet restore "./DeVesen.Bazaar/Server/DeVesen.Bazaar.Server.csproj"
COPY . .
WORKDIR "/src/DeVesen.Bazaar/Server"
RUN dotnet build "./DeVesen.Bazaar.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DeVesen.Bazaar.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /p:CopyOutputSymbolsToPublishDirectory=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeVesen.Bazaar.Server.dll"]
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MusicStore.Api/MusicStore.Api.csproj", "MusicStore.Api/"]
COPY ["MusicStore.Dto/MusicStore.Dto.csproj", "MusicStore.Dto/"]
COPY ["MusicStore.Entities/MusicStore.Entities.csproj", "MusicStore.Entities/"]
COPY ["MusicStore.Persistence/MusicStore.Persistence.csproj", "MusicStore.Persistence/"]
COPY ["MusicStore.Repositories/MusicStore.Repositories.csproj", "MusicStore.Repositories/"]
COPY ["MusicStore.Services/MusicStore.Services.csproj", "MusicStore.Services/"]
RUN dotnet restore "MusicStore.Api/MusicStore.Api.csproj"
COPY . .
WORKDIR "/src/MusicStore.Api"
RUN dotnet build "MusicStore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicStore.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicStore.Api.dll"]
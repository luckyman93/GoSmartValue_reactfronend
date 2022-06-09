#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GoSmartValue.Web/GoSmartValue.Web.csproj", "GoSmartValue.Web/"]
COPY ["AV.Handlers/AV.Handlers.csproj", "AV.Handlers/"]
COPY ["AV.Persistence/AV.Persistence.csproj", "AV.Persistence/"]
COPY ["AV.Common/AV.Common.csproj", "AV.Common/"]
COPY ["AV.Contracts/AV.Contracts.csproj", "AV.Contracts/"]
COPY ["AV.Persistence.EntityFramework/AV.Persistence.EntityFramework.csproj", "AV.Persistence.EntityFramework/"]
COPY ["AV.Infrastructure.Services/AV.Infrastructure.Services.csproj", "AV.Infrastructure.Services/"]
RUN dotnet restore "GoSmartValue.Web/GoSmartValue.Web.csproj"
COPY . .

RUN rm -rf GoSmartValue.Web/ClientApp
RUN mkdir GoSmartValue.Web/ClientApp

WORKDIR "/src/GoSmartValue.Web"
RUN dotnet build "GoSmartValue.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoSmartValue.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoSmartValue.Web.dll"]
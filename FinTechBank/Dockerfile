#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FinTechBank/FinTechBank.csproj", "FinTechBank/"]
COPY ["FinTechBank.BusinessLogic/FinTechBank.BusinessLogic.csproj", "FinTechBank.BusinessLogic/"]
COPY ["FinTechBank.Domain/FinTechBank.Domain.csproj", "FinTechBank.Domain/"]
COPY ["FinTechBank.DataAccess/FinTechBank.DataAccess.csproj", "FinTechBank.DataAccess/"]
COPY ["FinTechBank.Test/FinTechBank.Test.csproj", "FinTechBank.Test/"]
RUN dotnet restore "./FinTechBank/FinTechBank.csproj"
COPY . .
WORKDIR "/src/FinTechBank"
RUN dotnet build "./FinTechBank.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FinTechBank.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinTechBank.dll"]
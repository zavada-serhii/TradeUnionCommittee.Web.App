#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TradeUnionCommittee.Api/src/TradeUnionCommittee.Api/TradeUnionCommittee.Api.csproj", "TradeUnionCommittee.Api/src/TradeUnionCommittee.Api/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.ViewModels/TradeUnionCommittee.ViewModels.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.ViewModels/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.BLL/TradeUnionCommittee.BLL.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.BLL/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL/TradeUnionCommittee.DAL.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL.Identity/TradeUnionCommittee.DAL.Identity.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL.Identity/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.DataAnalysis.Service/TradeUnionCommittee.DataAnalysis.Service.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.DataAnalysis.Service/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.PDF.Service/TradeUnionCommittee.PDF.Service.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.PDF.Service/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.CloudStorage.Service/TradeUnionCommittee.CloudStorage.Service.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.CloudStorage.Service/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL.CloudStorage/TradeUnionCommittee.DAL.CloudStorage.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL.CloudStorage/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL.Audit/TradeUnionCommittee.DAL.Audit.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL.Audit/"]
RUN dotnet restore "TradeUnionCommittee.Api/src/TradeUnionCommittee.Api/TradeUnionCommittee.Api.csproj"
COPY . .
WORKDIR "/src/TradeUnionCommittee.Api"
RUN dotnet build "/src/TradeUnionCommittee.Api/src/TradeUnionCommittee.Api/TradeUnionCommittee.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "/src/TradeUnionCommittee.Api/src/TradeUnionCommittee.Api/TradeUnionCommittee.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TradeUnionCommittee.Api.dll"]
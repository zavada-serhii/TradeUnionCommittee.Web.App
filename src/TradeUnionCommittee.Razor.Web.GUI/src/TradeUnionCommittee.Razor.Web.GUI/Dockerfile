FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["TradeUnionCommittee.Razor.Web.GUI/src/TradeUnionCommittee.Razor.Web.GUI/TradeUnionCommittee.Razor.Web.GUI.csproj", "TradeUnionCommittee.Razor.Web.GUI/src/TradeUnionCommittee.Razor.Web.GUI/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.ViewModels/TradeUnionCommittee.ViewModels.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.ViewModels/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.BLL/TradeUnionCommittee.BLL.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.BLL/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL/TradeUnionCommittee.DAL.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL.Identity/TradeUnionCommittee.DAL.Identity.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL.Identity/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.DataAnalysis.Service/TradeUnionCommittee.DataAnalysis.Service.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.DataAnalysis.Service/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.PDF.Service/TradeUnionCommittee.PDF.Service.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.PDF.Service/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.CloudStorage.Service/TradeUnionCommittee.CloudStorage.Service.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.CloudStorage.Service/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL.CloudStorage/TradeUnionCommittee.DAL.CloudStorage.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL.CloudStorage/"]
COPY ["TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL.Audit/TradeUnionCommittee.DAL.Audit.csproj", "TradeUnionCommittee.Core/src/TradeUnionCommittee.DAL.Audit/"]
RUN dotnet restore "TradeUnionCommittee.Razor.Web.GUI/src/TradeUnionCommittee.Razor.Web.GUI/TradeUnionCommittee.Razor.Web.GUI.csproj"
COPY . .
WORKDIR "/src/TradeUnionCommittee.Razor.Web.GUI"
RUN dotnet build "/src/TradeUnionCommittee.Razor.Web.GUI/src/TradeUnionCommittee.Razor.Web.GUI/TradeUnionCommittee.Razor.Web.GUI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "/src/TradeUnionCommittee.Razor.Web.GUI/src/TradeUnionCommittee.Razor.Web.GUI/TradeUnionCommittee.Razor.Web.GUI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TradeUnionCommittee.Razor.Web.GUI.dll"]
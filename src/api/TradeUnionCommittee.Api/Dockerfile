FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["TradeUnionCommittee.Api/TradeUnionCommittee.Api.csproj", "TradeUnionCommittee.Api/"]
COPY ["TradeUnionCommittee.ViewModels/TradeUnionCommittee.ViewModels.csproj", "TradeUnionCommittee.ViewModels/"]
COPY ["TradeUnionCommittee.BLL/TradeUnionCommittee.BLL.csproj", "TradeUnionCommittee.BLL/"]
COPY ["TradeUnionCommittee.DAL/TradeUnionCommittee.DAL.csproj", "TradeUnionCommittee.DAL/"]
COPY ["TradeUnionCommittee.DAL.Identity/TradeUnionCommittee.DAL.Identity.csproj", "TradeUnionCommittee.DAL.Identity/"]
COPY ["TradeUnionCommittee.DataAnalysis.Service/TradeUnionCommittee.DataAnalysis.Service.csproj", "TradeUnionCommittee.DataAnalysis.Service/"]
COPY ["TradeUnionCommittee.PDF.Service/TradeUnionCommittee.PDF.Service.csproj", "TradeUnionCommittee.PDF.Service/"]
COPY ["TradeUnionCommittee.CloudStorage.Service/TradeUnionCommittee.CloudStorage.Service.csproj", "TradeUnionCommittee.CloudStorage.Service/"]
COPY ["TradeUnionCommittee.DAL.CloudStorage/TradeUnionCommittee.DAL.CloudStorage.csproj", "TradeUnionCommittee.DAL.CloudStorage/"]
COPY ["TradeUnionCommittee.DAL.Audit/TradeUnionCommittee.DAL.Audit.csproj", "TradeUnionCommittee.DAL.Audit/"]
RUN dotnet restore "TradeUnionCommittee.Api/TradeUnionCommittee.Api.csproj"
COPY . .
WORKDIR "/src/TradeUnionCommittee.Api"
RUN dotnet build "TradeUnionCommittee.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TradeUnionCommittee.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TradeUnionCommittee.Api.dll"]
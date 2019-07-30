FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["api/src/deprecated/TradeUnionCommittee.Mvc.Web.GUI/TradeUnionCommittee.Mvc.Web.GUI.csproj", "api/src/deprecated/TradeUnionCommittee.Mvc.Web.GUI/"]
COPY ["api/src/TradeUnionCommittee.ViewModels/TradeUnionCommittee.ViewModels.csproj", "api/src/TradeUnionCommittee.ViewModels/"]
COPY ["api/src/TradeUnionCommittee.BLL/TradeUnionCommittee.BLL.csproj", "api/src/TradeUnionCommittee.BLL/"]
COPY ["api/src/TradeUnionCommittee.DAL/TradeUnionCommittee.DAL.csproj", "api/src/TradeUnionCommittee.DAL/"]
COPY ["api/src/TradeUnionCommittee.DAL.Identity/TradeUnionCommittee.DAL.Identity.csproj", "api/src/TradeUnionCommittee.DAL.Identity/"]
COPY ["api/src/TradeUnionCommittee.DataAnalysis.Service/TradeUnionCommittee.DataAnalysis.Service.csproj", "api/src/TradeUnionCommittee.DataAnalysis.Service/"]
COPY ["api/src/TradeUnionCommittee.PDF.Service/TradeUnionCommittee.PDF.Service.csproj", "api/src/TradeUnionCommittee.PDF.Service/"]
COPY ["api/src/TradeUnionCommittee.CloudStorage.Service/TradeUnionCommittee.CloudStorage.Service.csproj", "api/src/TradeUnionCommittee.CloudStorage.Service/"]
COPY ["api/src/TradeUnionCommittee.DAL.CloudStorage/TradeUnionCommittee.DAL.CloudStorage.csproj", "api/src/TradeUnionCommittee.DAL.CloudStorage/"]
COPY ["api/src/TradeUnionCommittee.DAL.Audit/TradeUnionCommittee.DAL.Audit.csproj", "api/src/TradeUnionCommittee.DAL.Audit/"]
RUN dotnet restore "api/src/deprecated/TradeUnionCommittee.Mvc.Web.GUI/TradeUnionCommittee.Mvc.Web.GUI.csproj"
COPY . .
WORKDIR "/src/deprecated/TradeUnionCommittee.Mvc.Web.GUI"
RUN dotnet build "/src/api/src/deprecated/TradeUnionCommittee.Mvc.Web.GUI/TradeUnionCommittee.Mvc.Web.GUI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "/src/api/src/deprecated/TradeUnionCommittee.Mvc.Web.GUI/TradeUnionCommittee.Mvc.Web.GUI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TradeUnionCommittee.Mvc.Web.GUI.dll"]
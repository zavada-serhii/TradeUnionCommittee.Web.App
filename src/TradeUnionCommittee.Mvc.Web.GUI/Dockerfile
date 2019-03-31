FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/TradeUnionCommittee.Mvc.Web.GUI/TradeUnionCommittee.Mvc.Web.GUI.csproj src/TradeUnionCommittee.Mvc.Web.GUI/
COPY src/TradeUnionCommittee.Common/TradeUnionCommittee.Common.csproj src/TradeUnionCommittee.Common/
COPY src/TradeUnionCommittee.ViewModels/TradeUnionCommittee.ViewModels.csproj src/TradeUnionCommittee.ViewModels/
COPY src/TradeUnionCommittee.BLL/TradeUnionCommittee.BLL.csproj src/TradeUnionCommittee.BLL/
COPY src/TradeUnionCommittee.DAL/TradeUnionCommittee.DAL.csproj src/TradeUnionCommittee.DAL/
COPY src/TradeUnionCommittee.DataAnalysis.Service/TradeUnionCommittee.DataAnalysis.Service.csproj src/TradeUnionCommittee.DataAnalysis.Service/
COPY src/TradeUnionCommittee.PDF.Service/TradeUnionCommittee.PDF.Service.csproj src/TradeUnionCommittee.PDF.Service/
RUN dotnet restore src/TradeUnionCommittee.Mvc.Web.GUI/TradeUnionCommittee.Mvc.Web.GUI.csproj
COPY . .
WORKDIR /src/src/TradeUnionCommittee.Mvc.Web.GUI
RUN dotnet build TradeUnionCommittee.Mvc.Web.GUI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TradeUnionCommittee.Mvc.Web.GUI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TradeUnionCommittee.Mvc.Web.GUI.dll"]

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/TradeUnionCommittee.Razor.Web.GUI/TradeUnionCommittee.Razor.Web.GUI.csproj", "src/TradeUnionCommittee.Razor.Web.GUI/"]
RUN dotnet restore "src/TradeUnionCommittee.Razor.Web.GUI/TradeUnionCommittee.Razor.Web.GUI.csproj"
COPY . .
WORKDIR "/src/src/TradeUnionCommittee.Razor.Web.GUI"
RUN dotnet build "TradeUnionCommittee.Razor.Web.GUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TradeUnionCommittee.Razor.Web.GUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TradeUnionCommittee.Razor.Web.GUI.dll"]
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY DotNetDashboard.API/DotNetDashboard.API.csproj DotNetDashboard.API/
RUN dotnet restore DotNetDashboard.API/DotNetDashboard.API.csproj
COPY DotNetDashboard.API/ DotNetDashboard.API/
WORKDIR /src/DotNetDashboard.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DotNetDashboard.API.dll"]

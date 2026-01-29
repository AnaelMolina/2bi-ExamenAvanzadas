FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ApiVehiculo/ApiVehiculo.csproj", "ApiVehiculo/"]
RUN dotnet restore "ApiVehiculo/ApiVehiculo.csproj"

COPY . .
WORKDIR "/src/ApiVehiculo"
RUN dotnet build "ApiVehiculo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiVehiculo.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5004
ENV ASPNETCORE_URLS=http://+:5004

ENTRYPOINT ["dotnet", "ApiVehiculo.dll"]

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ApiCliente/ApiCliente.csproj", "ApiCliente/"]
RUN dotnet restore "ApiCliente/ApiCliente.csproj"

COPY . .
WORKDIR "/src/ApiCliente"
RUN dotnet build "ApiCliente.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiCliente.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5005
ENV ASPNETCORE_URLS=http://+:5005

ENTRYPOINT ["dotnet", "ApiCliente.dll"]

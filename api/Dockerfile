FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY . ./

RUN dotnet restore Backend/Backend.csproj --disable-parallel
RUN dotnet build Backend/Backend.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Backend/Backend.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Backend.dll"]
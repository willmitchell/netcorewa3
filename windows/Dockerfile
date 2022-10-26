FROM mcr.microsoft.com/windows/servercore:ltsc2022 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["netcorewa3.csproj", "./"]
RUN dotnet restore "netcorewa3.csproj"
COPY .. .
WORKDIR "/src/"
RUN dotnet build "netcorewa3.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -r win-x64 "netcorewa3.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish /app
ENTRYPOINT ["netcorewa3.exe"]

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["YarpSimple/YarpSimple.csproj", "YarpSimple/"]
RUN dotnet restore "YarpSimple/YarpSimple.csproj"
COPY . .
WORKDIR "/src/YarpSimple"
RUN dotnet build "YarpSimple.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YarpSimple.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YarpSimple.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["metrics-humidity/metrics-humidity.csproj", "metrics-humidity/"]
RUN dotnet restore "metrics-humidity/metrics-humidity.csproj"
COPY . .
WORKDIR "/src/metrics-humidity"
RUN dotnet build "metrics-humidity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "metrics-humidity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "metrics-humidity.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AsyncDemo.Api/AsyncDemo.Api.csproj", "AsyncDemo.Api/"]
RUN dotnet restore "AsyncDemo.Api/AsyncDemo.Api.csproj"
COPY . .
WORKDIR "/src/AsyncDemo.Api"
RUN dotnet build "AsyncDemo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AsyncDemo.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AsyncDemo.Api.dll"]

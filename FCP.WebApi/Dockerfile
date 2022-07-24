FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FCP.WebApi/FCP.WebApi.csproj", "FCP.WebApi/"]
RUN dotnet restore "FCP.WebApi/FCP.WebApi.csproj"
COPY . .
WORKDIR "/src/FCP.WebApi"
RUN dotnet build "FCP.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FCP.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCP.WebApi.dll"]

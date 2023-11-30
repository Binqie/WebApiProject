FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApiProject/WebApiProject.csproj", "WebApiProject/"]
RUN dotnet restore "WebApiProject/WebApiProject.csproj"
COPY . .
WORKDIR "/src/WebApiProject"
RUN dotnet build "WebApiProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiProject.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiProject.dll"]

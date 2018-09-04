FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 3388
EXPOSE 44390

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["RequestLogger/RequestLogger.csproj", "RequestLogger/"]
RUN dotnet restore "RequestLogger/RequestLogger.csproj"
COPY . .
WORKDIR "/src/RequestLogger"
RUN dotnet build "RequestLogger.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RequestLogger.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RequestLogger.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Service/Service.csproj", "Service/"]
COPY ["src/Domain/Domain.csproj", "Domain/"]
COPY ["Test/Test.csproj", "Test/"]  # Add Tests project
RUN dotnet restore "Service/Service.csproj"
RUN dotnet restore "Test/Test.csproj"  # Restore dependencies for tests
COPY . .
WORKDIR "/src/Service"
RUN dotnet build "Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/Test/bin/Release/netcoreapp3.1/  /app/test  # Copy test runner

# Add additional test runner arguments if needed (e.g., for code coverage)
ENTRYPOINT ["dotnet", "test", "/app/test/*.dll"]
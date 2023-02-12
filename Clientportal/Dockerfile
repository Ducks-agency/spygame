FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# first copy only csproj files, to make restore and cache restored intermediate image
COPY ["Clientportal/src/DucksAgency.Spygame.Clientportal/DucksAgency.Spygame.Clientportal.csproj", "Clientportal/src/DucksAgency.Spygame.Clientportal/"]
COPY ["Clientportal/tests/DucksAgency.Spygame.Clientportal.Tests/DucksAgency.Spygame.Clientportal.Tests.csproj", "Clientportal/tests/DucksAgency.Spygame.Clientportal.Tests/"]
RUN dotnet restore "Clientportal/src/DucksAgency.Spygame.Clientportal/DucksAgency.Spygame.Clientportal.csproj"
RUN dotnet restore "Clientportal/tests/DucksAgency.Spygame.Clientportal.Tests/DucksAgency.Spygame.Clientportal.Tests.csproj"

# copy all other source files to run build
COPY ["Clientportal/src/DucksAgency.Spygame.Clientportal/", "Clientportal/src/DucksAgency.Spygame.Clientportal/"]
COPY ["Clientportal/tests/DucksAgency.Spygame.Clientportal.Tests/", "Clientportal/tests/DucksAgency.Spygame.Clientportal.Tests/"]
WORKDIR "/src/Clientportal/src/DucksAgency.Spygame.Clientportal"
RUN dotnet build "DucksAgency.Spygame.Clientportal.csproj" -c Release -o /app/build --no-restore

# run tests
WORKDIR "/src/Clientportal/tests/DucksAgency.Spygame.Clientportal.Tests/"
RUN dotnet test "DucksAgency.Spygame.Clientportal.Tests.csproj"

#publish app
FROM build AS publish
WORKDIR "/src/Clientportal/src/DucksAgency.Spygame.Clientportal"
RUN dotnet publish "DucksAgency.Spygame.Clientportal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DucksAgency.Spygame.Clientportal.dll"]

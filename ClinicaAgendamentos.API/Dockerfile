FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClinicaAgendamentos.API/ClinicaAgendamentos.API.csproj", "ClinicaAgendamentos.API/"]
COPY ["ClinicaAgendamentos.Application/ClinicaAgendamentos.Application.csproj", "ClinicaAgendamentos.Application/"]
COPY ["ClinicaAgendamentos.Domain/ClinicaAgendamentos.Domain.csproj", "ClinicaAgendamentos.Domain/"]
COPY ["ClinicaAgendamentos.Infrastructure/ClinicaAgendamentos.Infrastructure.csproj", "ClinicaAgendamentos.Infrastructure/"]
RUN dotnet restore "ClinicaAgendamentos.API/ClinicaAgendamentos.API.csproj"
COPY . .
WORKDIR "/src/ClinicaAgendamentos.API"
RUN dotnet build "./ClinicaAgendamentos.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ClinicaAgendamentos.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/data
ENTRYPOINT ["dotnet", "ClinicaAgendamentos.API.dll"]

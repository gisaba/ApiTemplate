#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
ENV LD_LIBRARY_PATH="/app/clidriver/lib" 
ENV ASPNETCORE_ENVIRONMENT="Test"
RUN sed -i 's/SECLEVEL=2/SECLEVEL=1/g' /etc/ssl/openssl.cnf
#RUN apt-get -y update && apt-get install -y libxml2

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . /src
WORKDIR /src
COPY ["RockApi/RockApi.csproj", "RockApi/"]
COPY ["CompositionRoot/CompositionRoot.csproj", "CompositionRoot/"]
COPY ["CQRS/CQRS.csproj", "CQRS/"]
COPY ["Logging/Logging.csproj", "Logging/"]
COPY ["Persintence.EF/Persistence.EF.csproj", "Persintence.EF/"]
COPY ["DomainModel/DomainModel.csproj", "DomainModel/"]
RUN dotnet restore "RockApi/RockApi.csproj"
COPY . .
WORKDIR "/src/RockApi"
RUN dotnet build "RockApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RockApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RockApi.dll"]

#dotnet publish ./RockApi/RockApi.csproj -c Release -o ./RockApi/docker/api

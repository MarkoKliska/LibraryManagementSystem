FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/Api/LibraryManagementSystem.Api/LibraryManagementSystem.Api.csproj src/Api/LibraryManagementSystem.Api/
COPY src/Api/LibraryManagementSystem.Application/LibraryManagementSystem.Application.csproj src/Api/LibraryManagementSystem.Application/
COPY src/Api/LibraryManagementSystem.Domain/LibraryManagementSystem.Domain.csproj src/Api/LibraryManagementSystem.Domain/
COPY src/Api/LibraryManagementSystem.Infrastructure/LibraryManagementSystem.Infrastructure.csproj src/Api/LibraryManagementSystem.Infrastructure/
COPY src/Mail.Service/LibraryManagementSystem.Contracts/LibraryManagementSystem.Contracts.csproj src/Mail.Service/LibraryManagementSystem.Contracts/

RUN dotnet restore src/Api/LibraryManagementSystem.Api/LibraryManagementSystem.Api.csproj

COPY src/Api/ src/Api/
COPY src/Mail.Service/LibraryManagementSystem.Contracts/ src/Mail.Service/LibraryManagementSystem.Contracts/

RUN dotnet publish src/Api/LibraryManagementSystem.Api/LibraryManagementSystem.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "LibraryManagementSystem.Api.dll"]
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/Mail.Service/LibraryManagementSystem.MailService/LibraryManagementSystem.MailService.csproj src/Mail.Service/LibraryManagementSystem.MailService/
COPY src/Mail.Service/LibraryManagementSystem.Contracts/LibraryManagementSystem.Contracts.csproj src/Mail.Service/LibraryManagementSystem.Contracts/

RUN dotnet restore src/Mail.Service/LibraryManagementSystem.MailService/LibraryManagementSystem.MailService.csproj

COPY src/Mail.Service/ src/Mail.Service/

RUN dotnet publish src/Mail.Service/LibraryManagementSystem.MailService/LibraryManagementSystem.MailService.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "LibraryManagementSystem.MailService.dll"]
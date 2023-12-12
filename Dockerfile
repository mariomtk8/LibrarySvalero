
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
COPY ./publish /app
EXPOSE 6648
ENTRYPOINT ["dotnet", "LibrarySvalero.Presentation.dll"]

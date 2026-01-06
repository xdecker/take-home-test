FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copiar solución y proyectos
COPY src.sln ./
COPY Fundo.Applications.WebApi/Fundo.Applications.WebApi.csproj Fundo.Applications.WebApi/
COPY Fundo.Services.Tests/Fundo.Services.Tests.csproj Fundo.Services.Tests/

# restore
RUN dotnet restore

# copiar el resto del código
COPY . .

# publicar API
RUN dotnet publish Fundo.Applications.WebApi/Fundo.Applications.WebApi.csproj -c Release -o /publish

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "Fundo.Applications.WebApi.dll"]

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY src.sln ./
COPY Fundo.Applications.WebApi/Fundo.Applications.WebApi.csproj Fundo.Applications.WebApi/
COPY Fundo.Services.Tests/Fundo.Services.Tests.csproj Fundo.Services.Tests/

RUN dotnet restore
COPY . .
RUN dotnet publish Fundo.Applications.WebApi/Fundo.Applications.WebApi.csproj -c Release -o /publish


FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim
WORKDIR /app


RUN apt-get update \
    && apt-get install -y --no-install-recommends libicu-dev \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /publish .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "Fundo.Applications.WebApi.dll"]

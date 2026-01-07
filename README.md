# Loan Management System - Technical Test

This repository contains a simple Loan Management System built as a part of a technical test

## Tech Stack
- Backend: .NET 6 / ASP.NET Core Web API
- Database: Sql Server
- Frontend: Angular
- Testing: xUnit (Unit + Integration)
- API Documentation: Swagger OpenAPI

## BACKEND

# How to Run

### Prerequisites
- .NET 6 SDK
- Docker (optional, if you want to run SQL Server + API in containers)


## Database

The application uses SQL Server with Entity Framework Core.
- Database is created automatically using EF Core migrations.
- A simple seed runs on startup to populate initial data for testing purposes.
**Important:** When using Docker, the API requires the database to exist before it can run the seed.


## Running the Backend

# Running Locally (without Docker)
1. Sql Server running locally.
2. Update connectionstring on appsettings.json
3. Run migrations to create the database and tables:
    ```dotnet ef database update```
4. The following endpoint should return 200 OK:
    GET -> "https://localhost:55290;http://localhost:55291"
    *note:* To change the port, update on launchsettings
5. Swagger is enabled in the Development environment and is the default landing page.


# Runing with Docker
1. Make sure you are in the backend folder containing __docker-compose.yml__
2. First start Sql Server container
    ```docker-compose up -d sqlserver```
    *note:* Wait a few seconds until the SQL Server container is fully ready.
3. Run migrations from your host machine (or inside a separate container with dotnet sdk)
    ```dotnet ef database update```
4. Start the API container:
    ```docker-compose up -d api```
5. With Docker, the API will be available at:
    Docker (container mapped to host): http://localhost:5001
6. To stop all containers:
    ```docker-compose down```



## Tests

To execute tests:
    ```dotnet test```

### Unit Tests
Unit test covers business logic on LoanService

### Integration tests
A basic integration test is included to validate API availability


# General Notes
- the database connection string for Docker is configured in docker-compose.yml environment variables.
- Swagger provides interactive documentation for all endpoints in Development environment
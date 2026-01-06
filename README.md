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
- SQL Server (local or Docker)


## Database

The application uses SQL Server with Entity Framework Core.

- Database is created automatically using EF Core migrations.
- A simple seed runs on startup to populate initial data for testing purposes.


## Running the Backend

To build the backend, navigate to the `src` folder and run:  
```sh
dotnet build
```

To run all tests:  
```sh
dotnet test
```

To start the main API:  
```sh
cd Fundo.Applications.WebApi  
dotnet run
```

The following endpoint should return **200 OK**:  
```http
GET -> https://localhost:5001/loan
```
**Swagger is enabled in Development environment and configured as the default landing page.**


## Running with Docker

From the backend folder:

```bash
docker-compose up --build
```

## Tests
### Unit Tests
Unit test covers business logic on LoanService

# Integration tests
A basic integration test is included to validate API availability
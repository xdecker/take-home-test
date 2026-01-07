# Loan Management System - Technical Test

This repository contains a simple Loan Management System built as a part of a technical test

## Tech Stack

- Backend: .NET 6 / ASP.NET Core Web API
- Database: SQL Server
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

1. SQL Server running locally.
2. Update connectionstring on appsettings.json
3. Run migrations to create the database and tables:
   `dotnet ef database update`
4. The following endpoint should return 200 OK:
   GET -> "https://localhost:55290;http://localhost:55291"
   _note:_ To change the port, update on launchsettings
5. Swagger is enabled in the Development environment and is the default landing page.

# Running with Docker

1. Make sure you are in the backend folder containing **docker-compose.yml**
2. First start SQL Server container
   `docker-compose up -d sqlserver`
   _note:_ Wait a few seconds until the SQL Server container is fully ready.
3. Run migrations from your host machine (or inside a separate container with dotnet sdk)
   `dotnet ef database update`
4. Start the API container:
   `docker-compose up -d api`
5. With Docker, the API will be available at:
   Docker (container mapped to host): http://localhost:5001
6. To stop all containers:
   `docker-compose down`

## Tests

To execute tests:
`dotnet test`

### Unit Tests

Unit tests covers business logic in LoanService

### Integration tests

Basic integration test are included to validate API availability

# General Notes

- the database connection string for Docker is configured in docker-compose.yml environment variables.
- Swagger provides interactive documentation for all endpoints in Development environment

## FRONTEND

# How to Run

### Prerequisites

- Node.js version >= 20.19.6
- npm (comes with Node.js)

# Steps

1. Navigate to the frontend folder
2. Install dependencies:
   `npm install`
3. Configure the url of API in file `/src/environments/environment.ts`
4. start the development server:
   `npm start`
5. Open browser at:
   http://localhost:4200

## Features Implemented

- List loans with status and balance
- Create loan (modal form)
- Add payment (modal form)
- Delete loan
- Toast notifications for success, error, and loading
- Table pagination and actions column
- Form validations (required fields, positive amounts)

## Notes

- Frontend uses Angular Material for UI components.
- Global toast service used for all notifications.
- Modals are standalone components for reusability.
- API integration is done with Angular services.

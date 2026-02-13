# SimpleAuthApi

**SimpleAuthApi** is a robust, production-ready foundation for handling user authentication using ASP.NET Identity and JWT (JSON Web Tokens). It's clean, containerized, and ready to be integrated into any modern microservice architecture.

## Tech Stack & Architecture

- **Framework:** .NET 10 (Bleeding edge)  
- **Identity:** ASP.NET Core Identity for user management  
- **Security:** JWT Bearer authentication  
- **Database:** PostgreSQL  
- **Persistence:** Entity Framework Core (Code First)  
- **Infrastructure:** Fully dockerized environment for seamless deployment  

## Getting Started

### 1. Configure Environment
Clone `.env.example` to `.env` and define your `POSTGRES_PASSWORD` and `JWT_KEY`:

```bash
cp .env.example .env

2. Spin up the Containers

Launch the API and Database services in detached mode:

docker-compose up -d

3. Apply Database Migrations

Ensure your schema is up to date with the latest Entity Framework migrations:

dotnet ef database update --project src/SimpleAuthApi.Infrastructure --startup-project src/SimpleAuthApi.WebApi

4. Explore the API

Navigate to http://localhost:5257/swagger
to explore the interactive documentation and test the endpoints.
Quick Testing (curl)
Register a New User

curl -X POST http://localhost:5257/api/auth/register \
-H "Content-Type: application/json" \
-d '{
  "username": "admin@example.com",
  "email": "admin@example.com",
  "password": "Password123!"
}'

Login (Obtain JWT)

curl -X POST http://localhost:5257/api/auth/login \
-H "Content-Type: application/json" \
-d '{
  "email": "admin@example.com",
  "password": "Password123!"
}'

Access Protected Endpoint

curl -i -X GET http://localhost:5257/api/Users/me \
-H "Authorization: Bearer YOUR_TOKEN_HERE"
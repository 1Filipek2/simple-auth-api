# Simple Auth API

A robust and lightweight RESTful API for handling user authentication and management. Built with .NET 10 and a clean architecture approach, this project serves as a production-ready foundation for services requiring JWT-based security. It's fully containerized using Docker for seamless setup and deployment.

## Features

*   **User Authentication:** Secure user registration and login endpoints.
*   **JWT Security:** JSON Web Token (JWT) generation and validation for authenticating API requests.
*   **User Management:** Protected endpoints for authenticated users to view, update, and delete their own profiles.
*   **Clean Architecture:** The solution is divided into Domain, Application, Infrastructure, and WebApi layers, promoting separation of concerns and maintainability.
*   **Containerized:** Comes with `Dockerfile` and `docker-compose.yml` for a consistent development and production environment.
*   **Data Persistence:** Uses Entity Framework Core with a code-first approach against a PostgreSQL database.
*   **Exception Handling:** Custom middleware for centralized and consistent error responses.
*   **API Documentation:** Integrated Swagger/OpenAPI for easy exploration and testing of endpoints.

## Tech Stack

*   **Framework:** .NET 10 / ASP.NET Core
*   **Database:** PostgreSQL
*   **ORM:** Entity Framework Core
*   **Authentication:** JWT Bearer Tokens
*   **Password Hashing:** BCrypt.Net
*   **Containerization:** Docker / Docker Compose

## Getting Started

Follow these instructions to get the project up and running on your local machine.

### Prerequisites

*   [.NET 10 SDK](https://dotnet.microsoft.com/download) (or newer)
*   [Docker](https://www.docker.com/products/docker-desktop/) and Docker Compose

### Installation and Setup

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/1Filipek2/simple-auth-api.git
    cd simple-auth-api
    ```

2.  **Configure Environment Variables:**
    Create a `.env` file in the root directory by copying the example file.

    ```bash
    cp .env.example .env
    ```

    Open the new `.env` file and set the following required values. Use strong, unique values for a production environment.

    ```env
    POSTGRES_PASSWORD=your_strong_postgres_password
    JWT_KEY=your_super_secret_jwt_key_that_is_long_and_random
    ```

3.  **Launch Services with Docker:**
    Use Docker Compose to build the API image and run the API and database containers.

    ```bash
    docker-compose up -d --build
    ```
    The API will be available at `http://localhost:5257`, and the PostgreSQL database will be exposed on `localhost:5433`.

4.  **Apply Database Migrations:**
    Run the Entity Framework migration command to set up the database schema.

    ```bash
    dotnet ef database update --project src/SimpleAuthApi.Infrastructure --startup-project src/SimpleAuthApi.WebApi
    ```

The application is now running and ready for use.

## API Usage

### Swagger Documentation

Once the application is running, you can explore and interact with all available endpoints via the Swagger UI at:
**`http://localhost:5257/swagger`**

### Example Requests (cURL)

Here are some `curl` examples to demonstrate the core functionality.

**1. Register a new user:**

```bash
curl -X POST http://localhost:5257/api/auth/register \
-H "Content-Type: application/json" \
-d '{
  "username": "testuser",
  "email": "test@example.com",
  "password": "Password123!"
}'
```

**2. Log in and get a JWT:**

```bash
curl -X POST http://localhost:5257/api/auth/login \
-H "Content-Type: application/json" \
-d '{
  "email": "test@example.com",
  "password": "Password123!"
}'
```
The response will contain the JWT in a `token` field.

**3. Access a protected endpoint:**
Replace `YOUR_TOKEN_HERE` with the token received from the login endpoint.

```bash
curl -X GET http://localhost:5257/api/Users/me \
-H "Authorization: Bearer YOUR_TOKEN_HERE"
```

## Project Structure

The project follows a clean architecture pattern to create a decoupled and scalable system.

*   `src/SimpleAuthApi.Domain`: Contains the core business entities (e.g., `User`) and has no dependencies on other layers.
*   `src/SimpleAuthApi.Application`: Defines application logic, DTOs, interfaces (contracts for services like `IAuthService`), and custom exceptions. It depends only on the Domain layer.
*   `src/SimpleAuthApi.Infrastructure`: Provides implementations for the interfaces defined in the Application layer. This includes the `ApplicationDbContext`, authentication services, and password hashing logic.
*   `src/SimpleAuthApi.WebApi`: The presentation layer. It exposes the RESTful API endpoints, configures dependency injection, and contains middleware. It references the Application and Infrastructure layers.
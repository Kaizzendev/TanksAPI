# TanksAPI

Backend REST API for **[Tanks](https://github.com/Kaizzendev/Tanks)**, a roguelite tank game developed with Unity.

This project provides the backend services required by the game, including user authentication, persistent save games and leaderboard management.

The API is built with **`ASP.NET Core` and .NET 10**, using **PostgreSQL** as the database and **JWT** for authentication.

> 🚧 **Work in Progress**
>
> This API is currently under active development alongside the Tanks game.

---

## 🎮 About the Project

TanksAPI is the backend component of my **Tanks roguelite project**.

The game client is developed in Unity and communicates with this API to handle functionality that requires persistent data and server-side logic.

### Main responsibilities

* User registration and authentication
* JWT-based authorization
* Persistent save games
* Leaderboards
* Password hashing
* PostgreSQL database management
* Database migrations

---

## 🛠️ Technologies

| Technology                | Purpose                          |
| ------------------------- | -------------------------------- |
| **C#**                    | Programming language             |
| **.NET 10**               | Application framework            |
| **`ASP.NET Core`**          | REST API                         |
| **Entity Framework Core** | ORM and database access          |
| **PostgreSQL**            | Relational database              |
| **Npgsql**                | PostgreSQL provider for EF Core  |
| **JWT**                   | Authentication and authorization |
| **BCrypt**                | Password hashing                 |
| **Docker**                | Containerization                 |
| **OpenAPI / Scalar**      | API documentation                |

The project targets `net10.0` and uses Entity Framework Core with the PostgreSQL provider.

---

## 🏗️ Architecture

```text
┌──────────────────┐
│                  │
│   Unity Client   │
│                  │
└────────┬─────────┘
         │
         │ HTTP / REST
         ▼
┌──────────────────┐
│                  │
│    TanksAPI      │
│  ASP.NET Core    │
│                  │
└────────┬─────────┘
         │
         │ Entity Framework Core
         ▼
┌──────────────────┐
│                  │
│   PostgreSQL     │
│                  │
└──────────────────┘
```

The API uses `ASP.NET Core` controllers and Entity Framework Core to communicate with PostgreSQL. JWT authentication is configured as the default authentication scheme.

When running through Docker Compose, the API and database run as separate containers with PostgreSQL data persisted in a Docker volume.

---

## 📂 Project Structure

```text
TanksAPI/
│
├── Controllers/
│   ├── AuthController.cs
│   ├── LeaderboardController.cs
│   └── SaveGameController.cs
│
├── Data/
│   └── GameDbContext.cs
│
├── DTOs/
│
├── Migrations/
│
├── Models/
│
├── Services/
│   └── JwtService.cs
│
├── Program.cs
├── Dockerfile
├── docker-compose.yml
├── TanksAPI.csproj
└── README.md
```

### Controllers

**AuthController**

Handles user authentication and registration.

**SaveGameController**

Handles persistent game save data.

**LeaderboardController**

Provides leaderboard-related functionality.

The current API exposes these three controller areas in the `Controllers` directory.

---

## 🔐 Authentication

The API uses **JSON Web Tokens (JWT)** to authenticate users.

The authentication flow is:

```text
Client
  │
  │ Login credentials
  ▼
AuthController
  │
  ├── Validate credentials
  │
  ├── Verify BCrypt password hash
  │
  └── Generate JWT
  │
  ▼
Client
  │
  │ Authorization: Bearer <token>
  ▼
Protected endpoints
```

JWT validation checks:

* Issuer
* Audience
* Token lifetime
* Signing key

The API uses BCrypt for password hashing rather than storing plaintext passwords.

---

## 💾 Database

The API uses **PostgreSQL** with **Entity Framework Core**.

Database access is handled through:

```text
GameDbContext
       │
       ▼
Entity Framework Core
       │
       ▼
Npgsql
       │
       ▼
PostgreSQL
```

Database schema changes are managed using Entity Framework Core migrations.

---

## 🐳 Running with Docker

### Requirements

* Docker
* Docker Compose
* Git

Clone the repository:

```bash
git clone https://github.com/Kaizzendev/TanksAPI.git
cd TanksAPI
```

Create a `.env` file in the project root.

Example:

```env
POSTGRES_DB=tanks
POSTGRES_USER=tanks
POSTGRES_PASSWORD=your_password

JWT_KEY=your_secret_key
JWT_ISSUER=tanks-api
JWT_AUDIENCE=tanks-client
JWT_EXPIRE_MINUTES=60
```

Start the application:

```bash
docker compose up -d
```

This starts:

* `tanks-db` — PostgreSQL
* `tanks-api` — `ASP.NET Core` API

The default Docker configuration exposes PostgreSQL on port `5432` and the API on port `5024`.

To stop the containers:

```bash
docker compose down
```

The PostgreSQL data is stored in the `postgres_data` Docker volume, so stopping the containers does not remove the database data.

---

## 🧪 Local Development

Make sure PostgreSQL is running and the required connection string is configured.

Then run:

```bash
dotnet restore
dotnet build
dotnet run
```

The API uses `ASP.NET Core's` development environment to expose its OpenAPI documentation and Scalar API reference.

---

## 📖 API Documentation

When running in the **Development** environment, the project exposes OpenAPI documentation together with a Scalar API reference.

This allows endpoints to be explored and tested directly without requiring the Unity client.

---

## 🔄 Database Migrations

Create a new migration:

```bash
dotnet ef migrations add <MigrationName>
```

Apply migrations:

```bash
dotnet ef database update
```

Example:

```bash
dotnet ef migrations add AddLeaderboard
dotnet ef database update
```

---

## 📌 Current Features

* [x] `ASP.NET Core` REST API
* [x] PostgreSQL integration
* [x] Entity Framework Core
* [x] Docker support
* [x] User registration
* [x] JWT authentication
* [x] BCrypt password hashing
* [x] Save game persistence
* [x] Leaderboard system
* [x] Database migrations
* [x] OpenAPI documentation
* [x] Scalar API reference

---

## 🗺️ Roadmap

* [ ] Improve API validation
* [ ] Add automated tests
* [ ] Add integration tests
* [ ] Improve error handling
* [ ] Add refresh tokens
* [ ] Improve API documentation

---

## 🔗 Related Project

**Tanks** is the Unity game that consumes this API.

The game focuses on roguelite tank combat, procedural generation, enemy AI and player progression.

---

## 📜 Changelog

See [`CHANGELOG.md`](CHANGELOG.md) for a detailed history of changes.

The changelog follows the **Keep a Changelog** format.

---

## 📄 License

This project is licensed under the MIT License. See the `LICENSE` file for more information.

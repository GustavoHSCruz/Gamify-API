# Gamify API

Gamify API is a backend service designed to gamify user experiences, managing users, quests, and progression. Built with .NET and following Clean Architecture principles.

## 🚀 Technologies

- **.NET**: Core framework for the API.
- **ASP.NET Core**: Web API implementation.
- **Docker**: Containerization support.
- **Entity Framework Core**: ORM for database interactions (implied by typical .NET architecture).
- **Swagger**: API documentation and testing interface.

## 🏗 Architecture

The solution follows **Clean Architecture** principles to ensure separation of concerns and maintainability:

- **1.Service.API**: The entry point of the application (Controllers, Configuration).
- **2.Application**: Business logic, use cases, and service orchestration.
- **3.Domain.Core**: Core business entities, enums, events, and interfaces.
- **3.Domain.Shared**: Shared resources across the domain.
- **4.Infrastructure**: External concerns like database access, file systems, etc.

## ✨ Key Features

- **User Management**: Handle user profiles and data.
- **Quests System**: Manage and track quests for users.
- **Localization**: Support for multiple languages.

## 🛠 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop) (optional, for containerized run)

### Running Locally

1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```

2. Navigate to the API project directory:
   ```bash
   cd src/1.Service.API
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. Access Swagger UI:
   Open your browser and navigate to `https://localhost:7194/swagger` (port may vary based on launch settings).

### Running with Docker

Build and run the container:

```bash
docker build -t gamify-api .
docker run -p 8080:80 gamify-api
```

## 📝 TODO

Check [TODO.md](./TODO.md) for upcoming tasks and features.
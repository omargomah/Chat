```markdown
# MVC Chat Application

A real-time, web-based chat application built with ASP.NET Core MVC (.NET 10) and SignalR. The project utilizes Entity Framework Core, SQL Server, and modular backend architecture (DTOs, Services, and Repositories) for real-time messaging and user communication.

## Features

* **Real-Time Communication:** Instant messaging via ASP.NET Core SignalR Hubs.
* **MVC Architecture:** Server-rendered Razor views integrated with ASP.NET Core MVC on .NET 10.
* **Clean & Modular Structure:** Business logic separated into Controllers, Services, Repositories, Entities, and DTOs.
* **Database Management:** Entity Framework Core Code-First migrations for database state management.
* **Client-Side Asset Management:** Static file serving via `wwwroot` and library tracking with Library Manager (`libman.json`).

## Tech Stack

| Category | Technology |
| :--- | :--- |
| **Framework** | ASP.NET Core MVC (.NET 10) |
| **Language** | C# 14 |
| **Real-Time Messaging** | ASP.NET Core SignalR |
| **Database & ORM** | SQL Server, Entity Framework Core |
| **Frontend Assets** | Razor Views (CSHTML), HTML5, CSS, JavaScript |
| **Package Management** | NuGet, Library Manager (LibMan) |

## Prerequisites

Before running this project, ensure you have the following installed:

* **.NET 10 SDK**
* **Visual Studio 2022** (v17.12+ with .NET 10 workload) or **Visual Studio Code** (with C# Dev Kit)
* **SQL Server** (LocalDB, Express, or full SQL Server instance)

## Setup & Installation

### 1. Clone the Repository

```bash
git clone [https://github.com/omargomah/Chat.git](https://github.com/omargomah/Chat.git)
cd Chat

```

### 2. Configure Database Connection

Open `appsettings.json` in the root project directory and update the `ConnectionStrings` section to match your local SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MvcChatDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

```

### 3. Apply Database Migrations

Open your terminal or Package Manager Console in Visual Studio and run:

```bash
dotnet ef database update

```

### 4. Run the Application

#### Using .NET CLI:

```bash
dotnet run

```

#### Using Visual Studio:

1. Open `Chat.sln` or the `MVC.Chat` project.
2. Press `F5` or click **Start Debugging**.

The application will launch automatically in your browser (typically at `https://localhost:7000` or `http://localhost:5000`).

## Project Structure

```text
MVC.Chat/
├── Configurations/    # Entity and service configuration classes
├── Controllers/       # MVC controllers handling HTTP requests
├── Data/              # DbContext and EF Core database configurations
├── Dtos/              # Data Transfer Objects
├── Entities/          # Domain models / Database entities
├── Hubs/              # SignalR hubs for real-time WebSocket communication
├── Interfaces/        # Abstraction interfaces for services & repositories
├── Migrations/        # EF Core database migrations
├── Models/            # View models
├── Repositories/      # Data access layer implementations
├── Services/          # Business logic and application services
├── ValueObject/       # Domain value objects
├── Views/             # Razor view templates (.cshtml)
├── wwwroot/           # Static assets (CSS, JS, images, client libraries)
├── appsettings.json   # Configuration settings and connection strings
├── libman.json        # Client-side library dependency mapping
└── Program.cs         # Application entry point and dependency injection setup

```

## License

This project is open-source and available under the [MIT License](https://www.google.com/search?q=LICENSE).

```

```

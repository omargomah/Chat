<div align="center">

# Real-Time MVC Chat Application

**A real-time, full-stack web chat platform built with ASP.NET Core MVC (.NET 10) and SignalR.**

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20CORE-MVC-512BD4)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/EF%20CORE-10.0-239120)](https://learn.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20SERVER-DATABASE-CC292B)](https://www.microsoft.com/en-us/sql-server/)
[![Architecture](https://img.shields.io/badge/ARCHITECTURE-CLEAN-black)](https://learn.microsoft.com/en-us/dotnet/architecture/)

</div>

---

## Brief Description

The **MVC Chat Application** is a real-time web application designed to connect users through instant, bi-directional communication over WebSockets. Built on ASP.NET Core MVC using C# 14, it utilizes SignalR Hubs for low-latency message broadcasting and Entity Framework Core for structured database management.

> 📊 For more information, please explore the repository codebase and architecture configurations.

---

## Project Overview

This platform provides a complete end-to-end real-time chat service following a clean, layered architecture. It cleanly separates data access, business logic, web routing, and real-time socket events.

### Main Capabilities:

* **Real-Time Communication:** Instant messaging, live status updates, and group notifications via SignalR WebSockets.
* **Layered Architecture:** Decoupled responsibility using Controllers, Services, Repositories, Entities, and DTOs.
* **Data Persistence:** Managed relational database operations powered by Entity Framework Core Code-First migrations.
* **Client-Side Management:** Static asset pipelines via `wwwroot` and automated library tracking via Library Manager (`libman.json`).

---

## Tech Stack

| Category | Technology |
| :--- | :--- |
| **Framework** | ASP.NET Core MVC (.NET 10) |
| **Language** | C# 14 |
| **Real-Time Messaging** | ASP.NET Core SignalR |
| **Database & ORM** | SQL Server, Entity Framework Core |
| **Frontend** | Razor Views (`.cshtml`), HTML5, CSS3, JavaScript |
| **Package Management** | NuGet, Library Manager (`libman`) |

---

## Prerequisites

Ensure you have the following installed locally before running the application:

* **.NET 10 SDK**
* **Visual Studio 2022** (v17.12+ with *.NET 10 / Web Development* workload) or **VS Code** with C# Dev Kit
* **SQL Server** (LocalDB, Express, or standard instance)

---

## Getting Started

### 1. Clone the Repository

```bash
git clone [https://github.com/omargomah/Chat.git](https://github.com/omargomah/Chat.git)
cd Chat

```

### 2. Configure Connection String

Open `appsettings.json` and update your SQL Server connection details:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MvcChatDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

```

### 3. Database Setup

Apply Entity Framework Core migrations to create your local database schema:

```bash
dotnet ef database update

```

### 4. Run the Application

```bash
dotnet run

```

---

## Project Structure

```text
MVC.Chat/
├── Configurations/    # EF Core mappings and entity configuration setup
├── Controllers/       # MVC Web Controllers routing HTTP requests
├── Data/              # DbContext configurations and seed data setup
├── Dtos/              # Data Transfer Objects
├── Entities/          # Domain entity models
├── Hubs/              # SignalR Hubs handling WebSocket connections
├── Interfaces/        # Abstraction interfaces for services & repositories
├── Migrations/        # EF Core schema migration files
├── Models/            # Razor View Models
├── Repositories/      # Data access layer implementations
├── Services/          # Core application business logic
├── ValueObject/       # Domain Value Objects
├── Views/             # UI templates rendered with Razor (.cshtml)
├── wwwroot/           # Static files (CSS, JS, images, client packages)
├── appsettings.json   # Configuration settings & database connection strings
├── libman.json        # Client library dependency mapping file
└── Program.cs         # Application entry point & service dependency registration

```

```

```

<div align="center">

# Real-Time MVC Chat Application

**A real-time, full-stack web chat platform built with ASP.NET Core MVC (.NET 10) and SignalR.**

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core Identity](https://img.shields.io/badge/ASP.NET%20Core-Identity-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity)
[![EF Core](https://img.shields.io/badge/EF%20Core-10.0.10-239120?style=for-the-badge&logo=nuget&logoColor=white)](https://learn.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC292B?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/en-us/sql-server/)
[![Cloudinary](https://img.shields.io/badge/Cloudinary-Media%20Uploads-3448C5?style=for-the-badge&logo=cloudinary&logoColor=white)](https://cloudinary.com/)
[![MailKit](https://img.shields.io/badge/MailKit-Email%20Service-0078D4?style=for-the-badge)](https://github.com/jstedfast/MailKit)

</div>

---

## Brief Description

The **MVC Chat Application** is a real-time web platform designed to connect users through instant, bi-directional communication over WebSockets. Built on ASP.NET Core MVC using C# 14, it utilizes SignalR Hubs for low-latency message broadcasting, Entity Framework Core for structured data management, ASP.NET Core Identity for secure authentication, MailKit for email notifications, and Cloudinary for media asset management.

> 📊 For detailed architectural patterns and dependency configurations, please explore the repository codebase.

---

## Project Overview

This platform provides a complete end-to-end real-time chat service following a clean, layered architecture. It securely manages user data and cleanly separates data access, business logic, web routing, and real-time socket events.

### Main Capabilities:

* **Real-Time Communication:** Instant messaging, live status updates, and group notifications via SignalR WebSockets.
* **Authentication & Security:** Robust user identity management with ASP.NET Core Identity, protected by local `UserSecrets`.
* **Media & Attachments:** Cloud-based image and file hosting powered by `CloudinaryDotNet`.
* **Email Notifications:** Automated transactional email delivery (e.g., confirmations, resets) using `MailKit`.
* **Layered Architecture:** Decoupled responsibility using Controllers, Services, Repositories, Entities, and DTOs.
* **Data Persistence:** Managed relational database operations powered by Entity Framework Core Code-First migrations.

---

## Tech Stack

| Category | Technology |
| :--- | :--- |
| **Framework** | ASP.NET Core MVC (.NET 10) |
| **Language** | C# 14 |
| **Real-Time Messaging** | ASP.NET Core SignalR |
| **Authentication** | ASP.NET Core Identity |
| **Database & ORM** | SQL Server, Entity Framework Core 10.0.10 |
| **Cloud & External Services** | Cloudinary (v1.29.2), MailKit (v4.17.0) |
| **Frontend** | Razor Views (`.cshtml`), HTML5, CSS3, JavaScript |
| **Package Management** | NuGet, Library Manager (`libman.json`) |

---

## Prerequisites

Ensure you have the following installed locally before running the application:

* **.NET 10 SDK**
* **Visual Studio 2026 Community** (with the *.NET and Web Development* workload)
* **SQL Server** (LocalDB, Express, or standard instance)

---

## Getting Started

### 1. Clone the Repository

```bash
git clone [https://github.com/omargomah/Chat.git](https://github.com/omargomah/Chat.git)
cd Chat

```

### 2. Configure Database Connection

Open `appsettings.json` and update your SQL Server connection details:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MvcChatDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}

```

### 3. Configure External Services (User Secrets)

To keep your credentials secure, configure your Cloudinary and MailKit parameters using the .NET Secret Manager. Open your terminal in the project directory and run:

```bash
dotnet user-secrets set "Cloudinary:ApiSecret" "your_api_secret"

dotnet user-secrets set "EmailSettings:Password" "your_app_password"

```

### 4. Database Setup

Apply Entity Framework Core migrations to create your local database schema:

```bash
dotnet ef database update

```

### 5. Run the Application

1. Open `Chat.sln` or `MVC.Chat.csproj` in **Visual Studio 2026 Community**.
2. Wait for NuGet packages and `libman` client-side libraries to restore.
3. Press `F5` or click **Start Debugging** to launch the application.

---

## Project Structure

```text
MVC.Chat/
├── Configurations/    # EF Core entity mappings and fluent API setups
├── Controllers/       # MVC Web Controllers routing HTTP requests
├── Data/              # DbContext configuration and database sets
├── Dtos/              # Data Transfer Objects for client-server communication
├── Entities/          # Domain entity models representing database tables
├── Hubs/              # SignalR Hubs handling WebSocket connection lifecycles
├── Interfaces/        # Abstraction interfaces for Repositories and Services
├── Migrations/        # EF Core Code-First migration history
├── Models/            # Razor View Models for UI binding
├── Repositories/      # Data access layer implementations
├── Services/          # Core business logic (Email delivery, Media uploads)
├── ValueObject/       # Domain Value Objects
├── Views/             # UI templates rendered with Razor (.cshtml)
├── wwwroot/           # Static web assets (CSS, JS, images, client libraries)
├── appsettings.json   # Configuration settings & database connection strings
├── libman.json        # Client library dependency mapping file
├── Constants.cs       # Application-wide constant values
└── Program.cs         # Application entry point & service dependency registration

```

---

Open Source under the [MIT License](https://www.google.com/search?q=LICENSE)

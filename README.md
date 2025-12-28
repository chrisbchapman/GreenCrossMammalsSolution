# GreenCross.App Solution Template

A production-ready .NET 10 solution template with clean architecture for Windows Forms applications.

## Architecture

- **GreenCross.App.Entities** - Domain entities and base classes
- **GreenCross.App.Contracts** - Interfaces and contracts (Repositories, Services)
- **GreenCross.App.Data** - EF Core implementation (DbContext, Repositories, Migrations)
- **GreenCross.App.BLL** - Business logic layer with services
- **GreenCross.App.Bootstrap** - DI configuration and service registration
- **GreenCross.App.UI** - Windows Forms presentation layer

## Features

- ✅ Clean Architecture / Onion Architecture
- ✅ Entity Framework Core with SQL Server
- ✅ Repository Pattern with Unit of Work
- ✅ Dependency Injection
- ✅ High-performance logging with source generators
- ✅ DbContext pooling for performance
- ✅ Connection resiliency and retry logic
- ✅ Configuration management (appsettings.json)
- ✅ .NET 10 with C# 14

## Getting Started from Template

### Option 1: Use GitHub Template (Recommended)

1. Click **"Use this template"** button on GitHub
2. Create your new repository
3. Clone your new repository
4. Run the rename script:
```powershell
.\template-setup\rename-template.ps1 -NewName "YourCompany.YourApp"
```

### Option 2: Quick Start with Database Setup

```powershell
.\template-setup\quick-start.ps1 -ProjectName "YourCompany.YourApp" -DatabaseName "YourAppDb"
```

### Option 3: Manual Setup

1. Clone this repository
2. Run the PowerShell script:
```powershell
.\template-setup\rename-template.ps1 -NewName "YourCompany.YourApp"
```
3. Update connection string in `src\YourCompany.YourApp.UI\appsettings.json`
4. Clean and rebuild solution

## Database Setup

1. Update connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "AppDb": "Server=(localdb)\\mssqllocaldb;Database=YourAppDb;Trusted_Connection=true;TrustServerCertificate=true"
  }
}
```

2. Create initial migration:
```bash
dotnet ef migrations add InitialCreate --project src\YourApp.Data --startup-project src\YourApp.UI
```

3. Update database:
```bash
dotnet ef database update --project src\YourApp.Data --startup-project src\YourApp.UI
```

## Adding New Entities

1. Create entity in **Entities** project
2. Create repository interface in **Contracts** project
3. Implement repository in **Data** project
4. Create service interface in **Contracts** project
5. Implement service in **BLL** project
6. Register in **Bootstrap/ServiceRegistration.cs**
7. Add EF configuration in **Data/Configurations**

## Code Standards

- Follow `.editorconfig` rules (enforced)
- Use file-scoped namespaces
- Enable nullable reference types
- Use LoggerMessage source generators for high-performance logging
- Prefer `CancellationToken` support in async methods

## Requirements

- .NET 10 SDK
- Visual Studio 2022 (17.12+) or Visual Studio 2025
- SQL Server or LocalDB

## License

[Your License Here]

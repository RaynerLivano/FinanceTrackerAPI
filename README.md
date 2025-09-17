# Finance Tracker API

A simple **ASP.NET Core Web API** for tracking personal finance transactions using **Entity Framework Core** and **SQL Server**.  
Built as a learning project to practice backend development, migrations, and API documentation.

---

## Tech Stack
- ASP.NET Core 7 Web API
- Entity Framework Core
- SQL Server (LocalDB / MSSQL)
- Swagger (API documentation)

---

## Features
- Add new transactions (`POST /api/transactions`)
- Get all transactions (`GET /api/transactions`)
- Filter transactions by category (`GET /api/transactions/by-category/{category}`)
- Update a transaction (`PUT /api/transactions/{id}`)
- Delete a transaction (`DELETE /api/transactions/{id}`)
- Get balance summary (`GET /api/transactions/summary`)
- Get monthly summary (`GET /api/transactions/summary/month/{year}/{month}`)

(Planned: Authentication, User Accounts, Budgets & Reports)

---

## Getting Started

### 1. Clone the repository
```sh
git clone https://github.com/RaynerLivano/FinanceTrackerAPI.git
cd FinanceTrackerAPI
```

### 2. Restore dependencies

```sh
dotnet restore
```

### 3. Configure the database

Update your **appsettings.json** with your SQL Server connection string. Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=FinanceTrackerDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 4. Apply migrations

```sh
dotnet ef database update
```

### 5. Run the project

```sh
dotnet run
```

### 6. Open Swagger UI

Swagger will be available at:

* [http://localhost:5000/swagger](http://localhost:5000/swagger)
* [https://localhost:7000/swagger](https://localhost:7000/swagger) (for HTTPS)

```

---
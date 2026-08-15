# 🧠 Brain Of Company API

[![.NET](https://img.shields.io/badge/.NET-ASP.NET%20Core%20Web%20API-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Architecture](https://img.shields.io/badge/Architecture-N--Tier%20%2F%20Layered-blue?style=for-the-badge)](#-project-architecture)
[![Authentication](https://img.shields.io/badge/Auth-JWT%20%2B%20BCrypt-red?style=for-the-badge)](#-security--authentication)

An enterprise backend API designed for comprehensive organization management, handling employee payroll calculations, attendance tracking, hiring/firing analytics, department structures, dependent associations, category/product inventory, and invoicing.

---

## 🏗 Project Architecture

The solution uses an **N-Tier (Layered) Architecture** with the **Repository & Unit of Work** design patterns to separate database persistence from business logic and controllers.

```text
Brain_Of_Company_Solution/
├── Brain_API/             # Presentation Layer: API Controllers, DTOs, JWT Services
├── Brain_DAL/             # Data Access Layer: DbContext, Migrations, Repositories, UnitOfWork
├── Brain_Entities/        # Domain Layer: Core Database Models & Entities
└── ConsoleApp1/           # Utility / Helper Application

```

---

## 🛠 Tech Stack

| Category | Technology |
| --- | --- |
| **Framework** | ASP.NET Core Web API (.NET) |
| **Architecture** | N-Tier Layered Architecture |
| **Design Patterns** | Repository Pattern, Unit of Work (`IUnitOfWork`) |
| **Persistence & ORM** | Entity Framework Core |
| **Security & Auth** | JWT (JSON Web Tokens), `BCrypt.Net` Password Hashing |
| **Data Transfer** | Custom DTOs (Data Transfer Objects) |

---

## ✨ Core API Features & Endpoints

### 🔑 Authentication & Admin (`/api/Admin`)

* **Admin Auth:** Secure login generating JWT tokens (`/Login`).
* **Admin Accounts:** Full CRUD operation with hashed password storage using `BCrypt`.

### 👥 Employee & Payroll Management (`/api/Employee`)

* **Employee Records:** Lifecycle management supporting soft deletion (`IsDeleted`).
* **Salary Calculation:** Automated salary generation incorporating daily rate, attendance count, and percentage bonuses from product sales (`GetSalaryOfEmployee`).
* **HR Analytics:** Tracks hiring rates (`GetRateOfHiring`) and firing trends (`GetRateOfFiring`).

### 📅 Attendance Tracking (`/api/Attendance`)

* **Daily Logging:** Log employee attendance with duplicate daily entry prevention.
* **Attendance Analytics:** Calculates attendance rates (`GetRateOfAttendance`) and absence metrics (`GetTheAbsenceRate`).
* **Date Filtering:** Query attendance records by specific day or date range per employee SSN.

### 🏢 Departments & Dependents (`/api/Department`, `/api/Dependent`, `/api/EmployeeDependant`)

* **Department Hierarchy:** Tracks department managers, locations, working employees, and minimum required monthly attendance days.
* **Employee Dependents:** Manage family/dependent coverage linked to employee SSNs (`14-digit SSN validation`).

### 📦 Products, Categories & Invoices (`/api/Category`, `/api/Product`, `/api/Invoice`)

* **Inventory Control:** Categories and products with real stock vs. sold quantity tracking.
* **Invoicing:** Sales records that feed into employee bonus percentages.

---

## 🚀 Getting Started

### Prerequisites

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/)

### Setup & Execution

1. **Clone the repository:**
```bash
git clone [https://github.com/omargomah/Brain_Of_Company.git](https://github.com/omargomah/Brain_Of_Company.git)
cd Brain_Of_Company

```


2. **Configure Database Connection & JWT:**
Update the connection string and JWT secret inside `Brain_API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=BrainOfCompanyDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY_HERE",
    "Issuer": "Brain_API"
  }
}

```


3. **Apply Database Migrations:**
```bash
dotnet ef database update --project Brain_DAL --startup-project Brain_API

```


4. **Run the Application:**
```bash
dotnet run --project Brain_API

```



```

```

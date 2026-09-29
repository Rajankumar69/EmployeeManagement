# Employee Management Web API

An ASP.NET Core 9.0 Web API built adhering to **Clean Architecture**, **CQRS pattern**, **JWT Authentication**, **Role-Based and Policy-Based Authorization**, **Repository & Service patterns**, **SOLID principles**, **Global Exception Handling**, and **Custom Middleware**.

---

## 🏗️ Project Architecture (Clean Architecture)

```
c:\Users\rajankumar\source\repos\EmployeeManagmentWebApi\
├── src/
│   ├── EmployeeManagement.Domain/          # Core Domain Entities, Enums, & Domain Exceptions
│   ├── EmployeeManagement.Application/     # DTOs, Service Interfaces, CQRS Commands & Queries
│   ├── EmployeeManagement.Infrastructure/  # EF Core DbContext, Repositories, JWT Token Generator, Seeding
│   └── EmployeeManagement.Api/             # Controllers, Middlewares, Program.cs, AppSettings
└── tests/
    └── EmployeeManagement.Tests/           # Automated Unit & Integration Tests (xUnit)
```

---

## 🔑 Key Features Implemented

1. **REST APIs & Routing**:
   - `POST /api/auth/login` - Authenticate user & get JWT token
   - `GET /api/employees` - Retrieve all employees (*Admin, User, Manager*)
   - `GET /api/employees/{id}` - Retrieve employee by ID (*Admin, User, Manager*)
   - `POST /api/employees` - Create new employee (*Admin only*)
   - `PUT /api/employees/{id}` - Update employee (*Admin only*)
   - `DELETE /api/employees/{id}` - Delete employee (*Admin only*)
   - `GET /api/employees/management-audit` - Demonstration of **Policy-Based Authorization** (*Department Claim = Management*)

2. **Model Validation & DTOs**:
   - Request DTOs (`CreateEmployeeDto`, `UpdateEmployeeDto`, `LoginRequestDto`) with Data Annotations (`[Required]`, `[EmailAddress]`, `[StringLength]`, `[Range]`).
   - Separate Response DTOs (`EmployeeDto`, `LoginResponseDto`).

3. **JWT Authentication & Claims**:
   - Pre-seeded users for testing:
     - **Admin**: Username: `admin` | Password: `Admin@123` | Role: `Admin`
     - **User**: Username: `user` | Password: `User@123` | Role: `User`
     - **Manager**: Username: `manager` | Password: `Manager@123` | Role: `Manager` | Department: `Management`
   - Tokens contain claims for `NameIdentifier`, `Name`, `Email`, `Role`, and `Department`.

4. **CQRS Pattern**:
   - Separated Read query handler (`GetEmployeeByIdQueryHandler`) and Write command handler (`CreateEmployeeCommandHandler`).

5. **Custom Middlewares**:
   - `RequestResponseLoggingMiddleware`: Logs request details (HTTP Method, Path) and execution time.
   - `GlobalExceptionMiddleware`: Catches unhandled exceptions and converts them to standard `ProblemDetails` responses (HTTP 400, 404, 401, 500).

6. **Swagger / OpenAPI**:
   - Interactive Swagger UI configured with JWT Bearer authentication scheme.

---

## 🚀 How to Run the Application

### 1. Run the Web API
Open terminal in the workspace root and run:
```bash
dotnet run --project src/EmployeeManagement.Api
```

### 2. Access Swagger UI
Open your browser and navigate to:
```
http://localhost:5113/swagger
```

### 3. Testing Authorization in Swagger
1. Execute `POST /api/auth/login` with body:
   ```json
   {
     "username": "admin",
     "password": "Admin@123"
   }
   ```
2. Copy the generated `token` from the response.
3. Click the **Authorize** button at the top right of Swagger UI.
4. Type `Bearer <YOUR_COPIED_TOKEN>` (or paste token if prompted) and click **Authorize**.
5. Test the `GET`, `POST`, `PUT`, `DELETE` endpoints!

---

## 🧪 Running Unit & Integration Tests

Run the following command to execute all tests:
```bash
dotnet test
```

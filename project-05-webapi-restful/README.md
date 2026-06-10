# 🔴 Project 05 — RESTful Web API

**Độ khó:** ⭐⭐⭐⭐⭐  
**Chủ đề:** ASP.NET Core, Controllers, Entity Framework Core, Swagger, JWT

---

## 🎯 Mục tiêu

Xây dựng **Task Management REST API** — backend hoàn chỉnh với DB, Auth, Swagger docs.

---

## 📋 Chức năng cần làm

- [ ] CRUD Tasks (title, description, status, priority, dueDate)
- [ ] CRUD Users
- [ ] JWT Authentication (Register / Login / Protect routes)
- [ ] Filter tasks: theo status, priority, user
- [ ] Pagination (`?page=1&size=10`)
- [ ] Swagger UI (`/swagger`)
- [ ] Validation với Data Annotations
- [ ] Global exception handler middleware

---

## 🏗️ Cấu trúc project

```
project-05-webapi-restful/
├── Controllers/
│   ├── AuthController.cs
│   ├── TasksController.cs
│   └── UsersController.cs
├── Models/
│   ├── TaskItem.cs
│   └── User.cs
├── DTOs/
│   ├── TaskDto.cs
│   └── LoginDto.cs
├── Services/
│   ├── ITaskService.cs
│   └── TaskService.cs
├── Data/
│   └── AppDbContext.cs
├── Middleware/
│   └── ExceptionMiddleware.cs
├── Program.cs
└── appsettings.json
```

---

## 🚀 Khởi tạo

```bash
cd project-05-webapi-restful
dotnet new webapi
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet run
# Swagger: https://localhost:5001/swagger
```

---

## 💡 Kiến thức áp dụng

- ASP.NET Core Controllers, Routing
- Dependency Injection (`AddScoped`, `AddSingleton`)
- Entity Framework Core + SQLite (dễ setup)
- JWT Bearer Authentication
- Data Annotations (`[Required]`, `[StringLength]`)
- Middleware pipeline
- DTO pattern (không expose entity trực tiếp)

---

## 📖 Doc cần đọc trước

→ [`docs/05-aspnet-webapi.md`](../docs/05-aspnet-webapi.md)

---

## 🏆 Hoàn thành Project 05 = Bạn đã là .NET Web Developer!

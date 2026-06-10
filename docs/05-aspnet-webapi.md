# 📘 ASP.NET Core Web API

> Đây là đích đến — .NET Web Developer!

---

## 1. Tạo Web API project

```bash
dotnet new webapi -n MyWebApi
cd MyWebApi
dotnet run
# Truy cập: https://localhost:5001/swagger
```

---

## 2. Cấu trúc project Web API

```
MyWebApi/
├── Controllers/          ← Route handlers (giống Spring @RestController)
│   └── ProductsController.cs
├── Models/               ← Data models / Entities
│   └── Product.cs
├── Services/             ← Business logic
│   └── ProductService.cs
├── Data/                 ← DbContext, Migrations (EF Core)
│   └── AppDbContext.cs
├── appsettings.json      ← Config (giống application.properties)
├── Program.cs            ← Entry point, DI setup
└── MyWebApi.csproj
```

---

## 3. Controller

```csharp
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]           // api/products
public class ProductsController : ControllerBase {
    private readonly IProductService _service;
    
    // Dependency Injection — constructor injection
    public ProductsController(IProductService service) {
        _service = service;
    }
    
    // GET api/products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll() {
        var products = await _service.GetAllAsync();
        return Ok(products);           // 200
    }
    
    // GET api/products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id) {
        var product = await _service.GetByIdAsync(id);
        if (product == null) return NotFound();    // 404
        return Ok(product);
    }
    
    // POST api/products
    [HttpPost]
    public async Task<ActionResult<Product>> Create(ProductDto dto) {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);  // 201
    }
    
    // PUT api/products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductDto dto) {
        await _service.UpdateAsync(id, dto);
        return NoContent();            // 204
    }
    
    // DELETE api/products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
```

---

## 4. Entity Framework Core

```bash
# Cài packages
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Tạo migration
dotnet ef migrations add InitialCreate
dotnet ef database update
```

```csharp
// Model / Entity
public class Product {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// DbContext
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Product>(entity => {
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
        });
    }
}
```

---

## 5. Dependency Injection — Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);

// Đăng ký services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Custom services
builder.Services.AddScoped<IProductService, ProductService>();   // per request
builder.Services.AddSingleton<ICacheService, MemoryCacheService>(); // one instance
builder.Services.AddTransient<IEmailService, EmailService>();    // new each time

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

---

## 6. appsettings.json

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=MyDb;Trusted_Connection=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

---

## 🎯 Roadmap sau Project 05

```
ASP.NET Core Web API
    ↓
Authentication (JWT Bearer)
    ↓
Clean Architecture / CQRS
    ↓
Azure Deployment
    ↓
.NET Developer 🏆
```

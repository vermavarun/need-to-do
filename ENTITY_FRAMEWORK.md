dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer   # or .Sqlite / .Npgsql
dotnet add package Microsoft.EntityFrameworkCore.Tools       # for migrations CLI

---

Models/Todo.cs

namespace NeedToDo.APIs.Models;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

---

Data/ToDoDBContext.cs

using Microsoft.EntityFrameworkCore;
using NeedToDo.APIs.Models;

namespace NeedToDo.APIs.Data;

public class ToDoDBContext : DbContext
{
    public ToDoDBContext(DbContextOptions<ToDoDBContext> options) : base(options) { }

    public DbSet<Todo> Todos { get; set; }
}

---
appsettings.json

"ConnectionStrings": {
  "DefaultConnection": "Server=...;Database=needtodo;..."
}

---
Program.cs

using NeedToDo.APIs.Data;
using Microsoft.EntityFrameworkCore;

builder.Services.AddDbContext<ToDoDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

---
create and apply migrations

dotnet ef migrations add InitialCreate
dotnet ef database update
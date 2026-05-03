using Microsoft.EntityFrameworkCore;
using NeedToDo.APIs.Models;

namespace NeedToDo.APIs.Data;

public class ToDoDBContext : DbContext
{
    public ToDoDBContext(DbContextOptions<ToDoDBContext> options) : base(options) {}

    public DbSet<ToDo> ToDos {get;set;}
}
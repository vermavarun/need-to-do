using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeedToDo.APIs.Data;
using NeedToDo.APIs.Models;

namespace NeedToDo.APIs.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoController : ControllerBase
{
    private  ToDoDBContext _dbContext;
    public ToDoController(ToDoDBContext toDoDBContext)
    {
        _dbContext = toDoDBContext;
    }

    [HttpGet(Name = "GetToDo")]
    public IEnumerable<ToDo> Get()
    {

        return _dbContext.ToDos.ToList<ToDo>();

    }

    [HttpPost(Name = "AddTodo")]
    public HttpStatusCode Post(ToDo todo)
    {
        _dbContext.ToDos.Add(new ToDo(){Title=todo.Title});
        _dbContext.SaveChanges();
        return HttpStatusCode.Accepted;
    }
}

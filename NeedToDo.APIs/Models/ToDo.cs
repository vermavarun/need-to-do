using System.ComponentModel.DataAnnotations.Schema;

namespace NeedToDo.APIs.Models;

public class ToDo
{
public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    [Column("is_completed")]
    public bool IsCompleted { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
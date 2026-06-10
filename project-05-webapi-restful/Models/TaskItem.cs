namespace WebApiRestful.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public WorkStatus Status { get; set; } = WorkStatus.Todo;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }
    public User? User { get; set; }
}

public enum WorkStatus { Todo, InProgress, Done }

public enum TaskPriority { Low, Medium, High }
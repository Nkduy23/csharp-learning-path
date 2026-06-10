using System.ComponentModel.DataAnnotations;
using WebApiRestful.Models;

namespace WebApiRestful.DTOs;

public class CreateTaskDto
{
    [Required] [MinLength(3)] [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime? DueDate { get; set; }
}

public class UpdateTaskDto
{
    [MinLength(3)] [MaxLength(200)]
    public string? Title { get; set; }
    public string? Description { get; set; }
    public WorkStatus? Status { get; set; }
    public TaskPriority? Priority { get; set; }
    public DateTime? DueDate { get; set; }
}

public class TaskResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Owner { get; set; } = string.Empty;
}
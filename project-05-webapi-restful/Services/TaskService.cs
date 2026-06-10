using Microsoft.EntityFrameworkCore;
using WebApiRestful.Data;
using WebApiRestful.DTOs;
using WebApiRestful.Models;

namespace WebApiRestful.Services;

public interface ITaskService
{
    Task<List<TaskResponseDto>> GetAllAsync(int userId, WorkStatus? status, TaskPriority? priority, int page, int size);
    Task<TaskResponseDto?> GetByIdAsync(int id, int userId);
    Task<TaskResponseDto> CreateAsync(CreateTaskDto dto, int userId);
    Task<TaskResponseDto?> UpdateAsync(int id, UpdateTaskDto dto, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}

public class TaskService : ITaskService
{
    private readonly AppDbContext _db;

    public TaskService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TaskResponseDto>> GetAllAsync(
    int userId, WorkStatus? status, TaskPriority? priority, int page, int size)
    {
        var query = _db.Tasks
            .Include(t => t.User)
            .Where(t => t.UserId == userId);

        if (status != null)   query = query.Where(t => t.Status == status);
        if (priority != null) query = query.Where(t => t.Priority == priority);

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((page - 1) * size)
            .Take(size)
            .Select(t => ToDto(t))
            .ToListAsync();
    }

    public async Task<TaskResponseDto?> GetByIdAsync(int id, int userId)
    {
        var task = await _db.Tasks.Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        return task == null ? null : ToDto(task);
    }

    public async Task<TaskResponseDto> CreateAsync(CreateTaskDto dto, int userId)
    {
        var task = new TaskItem
        {
            Title       = dto.Title,
            Description = dto.Description,
            Priority    = dto.Priority,
            DueDate     = dto.DueDate,
            UserId      = userId
        };
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        await _db.Entry(task).Reference(t => t.User).LoadAsync();
        return ToDto(task);
    }

    public async Task<TaskResponseDto?> UpdateAsync(int id, UpdateTaskDto dto, int userId)
    {
        var task = await _db.Tasks.Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        if (task == null) return null;

        if (dto.Title       != null) task.Title       = dto.Title;
        if (dto.Description != null) task.Description = dto.Description;
        if (dto.Status      != null) task.Status      = dto.Status.Value;
        if (dto.Priority    != null) task.Priority    = dto.Priority.Value;
        if (dto.DueDate     != null) task.DueDate     = dto.DueDate;

        await _db.SaveChangesAsync();
        return ToDto(task);
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        if (task == null) return false;
        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return true;
    }

    private static TaskResponseDto ToDto(TaskItem t) => new()
    {
        Id          = t.Id,
        Title       = t.Title,
        Description = t.Description,
        Status      = t.Status.ToString(),
        Priority    = t.Priority.ToString(),
        DueDate     = t.DueDate,
        CreatedAt   = t.CreatedAt,
        Owner       = t.User?.Username ?? ""
    };
}
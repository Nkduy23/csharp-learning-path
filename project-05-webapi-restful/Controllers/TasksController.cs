using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiRestful.DTOs;
using WebApiRestful.Models;
using WebApiRestful.Services;

namespace WebApiRestful.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] WorkStatus? status,
        [FromQuery] TaskPriority? priority,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10)
    {
        var tasks = await _taskService.GetAllAsync(GetUserId(), status, priority, page, size);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id, GetUserId());
        return task == null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        var task = await _taskService.CreateAsync(dto, GetUserId());
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
    {
        var task = await _taskService.UpdateAsync(id, dto, GetUserId());
        return task == null ? NotFound() : Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _taskService.DeleteAsync(id, GetUserId());
        return deleted ? NoContent() : NotFound();
    }
}
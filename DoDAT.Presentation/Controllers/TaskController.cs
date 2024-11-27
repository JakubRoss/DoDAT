using DoDAT.Application.Interfaces;
using DoDAT.Domain.Entities;
using DoDAT.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tasks")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly IToDoitemService _toDoitemService;

    public TaskController( IToDoitemService toDoitemService)
    {
        _toDoitemService = toDoitemService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ToDoItemDto todo)
    {
        var userIdClaim = User.FindFirst(t => t.Type == "NameIdentifier")?.Value;
        if (int.TryParse(userIdClaim, out int userId))
        {
            var toDoItem = await _toDoitemService.AddToDoItemAsync(todo, userId);
            return Ok(toDoItem);
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }

    [HttpGet]
    public async Task<ActionResult<ToDoItem>> GetAll()
    {
        var userIdClaim = User.FindFirst(t => t.Type == "NameIdentifier")?.Value;
        if (int.TryParse(userIdClaim, out int userId))
        {
            var todos = await _toDoitemService.GetAllToDoItemsAsync(userId);
            return Ok(todos);
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }

    [HttpGet("{toDoItemId}")]
    public async Task<ActionResult<ToDoItem>> GetById(int toDoItemId)
    {
        var userIdClaim = User.FindFirst(t => t.Type == "NameIdentifier")?.Value;
        if (int.TryParse(userIdClaim, out int userId))
        {
            ToDoItem todo = await _toDoitemService.GetToDoItemByIdAsync(toDoItemId, userId);
            return Ok(todo);
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }

    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDate([FromQuery] string? selectedDate)
    {
        var userIdClaim = User.FindFirst(t => t.Type == "NameIdentifier")?.Value;
        if (!DateTime.TryParse(selectedDate, out DateTime dateTime))
            return BadRequest("Invalid date format.");

        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized("Invalid or missing user information.");

        var toDoItems = await _toDoitemService.GetToDoItemsByDateAsync(dateTime, userId);
        return Ok(toDoItems);
    }

    [HttpPut("{taskId}")]
    public async Task<IActionResult> Update(int taskId, [FromBody] ToDoItemDto payload)
    {
        //if (id != payload.Id)
        //    return BadRequest("Id Mismatch");
        var userIdClaim = User.FindFirst(t => t.Type == "NameIdentifier")?.Value;

        if (!int.TryParse(userIdClaim, out int userId))
        {
            throw new UnauthorizedAccessException();
        }
        await _toDoitemService.UpdateToDoItemAsync(payload, userId , taskId);
        return Ok();
    }

    [HttpDelete("{ToDoItemToDeleteId}")]
    public async Task<IActionResult> Delete(int ToDoItemToDeleteId)
    {
        var userIdClaim = User.FindFirst(t => t.Type == "NameIdentifier")?.Value;
        if (!int.TryParse(userIdClaim, out int userId))
        {
            throw new UnauthorizedAccessException();
        }

        await _toDoitemService.DeleteToDoItemAsync(ToDoItemToDeleteId, userId);
        return NoContent();
    }
}
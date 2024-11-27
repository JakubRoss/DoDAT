using DoDAT.Presentation.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/tasks")]
[Authorize]
public class HomeController : ControllerBase
{
    private readonly IToDoitemRepository _toDoitemRepository;

    public HomeController(IToDoitemRepository toDoitemRepository)
    {
        _toDoitemRepository = toDoitemRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ToDoItemDto todo)
    {
        var userIdClaim = User.FindFirst(t => t.Type == "NameIdentifier")?.Value;
        if (int.TryParse(userIdClaim, out int userId))
        {
            var toDoItem = await _toDoitemRepository.AddToDoItemAsync(todo, userId);
            return Ok(toDoItem);
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }

    [HttpGet]
    public async Task<ActionResult<ToDoItem>> GetAll(int id)
    {
        var todo = await _toDoitemRepository.GetAllToDoItemsAsync();
        return Ok(todo);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItem>> GetById(int id)
    {
        ToDoItem todo = await _toDoitemRepository.GetToDoItemByIdAsync(id);
        return Ok(todo);
    }

    [HttpGet("by-date")]
    public async Task<IEnumerable<ToDoItem>> GetByDate([FromQuery] string? selectedDate)
    {

        DateTime dateTime;

        if (DateTime.TryParse(selectedDate, out dateTime))
        {
            return await _toDoitemRepository.GetToDoItemsByDateAsync(dateTime);
        }
        return new List<ToDoItem>();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ToDoItem todo)
    {
        if (id != todo.Id)
            return BadRequest("Id Mismatch");

        await _toDoitemRepository.UpdateToDoItemAsync(todo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

        await _toDoitemRepository.DeleteToDoItemAsync(id);
        return NoContent();
    }
}
public class ToDoItemDto
{

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
    public string Title { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    [Required(ErrorMessage = "Due Date is required.")]
    public DateTime DueDate { get; set; }


}
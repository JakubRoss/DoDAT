using DoDAT.Presentation.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Create([FromBody] ToDoItem todo)
    {
            await _toDoitemRepository.AddToDoItemAsync(todo);
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        
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

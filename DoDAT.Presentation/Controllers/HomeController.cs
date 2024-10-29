using DoDAT.Presentation.Domain;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private IToDoitemRepository _toDoitemRepository;

    public HomeController(IToDoitemRepository toDoitemRepository)
    {
        _toDoitemRepository = toDoitemRepository;
    }

    public async Task<IActionResult> Index(string? selectedDate)
    {
        IEnumerable<ToDoItem> todos;

        DateTime dateTime;

        if (DateTime.TryParse(selectedDate, out dateTime))
        {
            todos = await _toDoitemRepository.GetToDoItemsByDateAsync(dateTime);
        }
        else
        {
            todos = await _toDoitemRepository.GetAllToDoItemsAsync();
        }

        return View(todos);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ToDoItem todo)
    {
        if (ModelState.IsValid)
        {
            await _toDoitemRepository.AddToDoItemAsync(todo);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var todo = await _toDoitemRepository.GetToDoItemByIdAsync(id);
        if (todo == null)
            return NotFound();

        return View(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ToDoItem todo)
    {
        if (ModelState.IsValid)
        {
            await _toDoitemRepository.UpdateToDoItemAsync(todo);
            return RedirectToAction("Index");
        }
        return View(todo);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _toDoitemRepository.DeleteToDoItemAsync(id);
        return RedirectToAction("Index");
    }
}

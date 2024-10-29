using DoDAT.Presentation.Domain;
using Microsoft.EntityFrameworkCore;

namespace DoDAT.Presentation.Infrastructure
{
    public class ToDoitemRepository : IToDoitemRepository
    {
        private readonly ApplicationDbContext _context;

        public ToDoitemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddToDoItemAsync(ToDoItem doTask)
        {
            await _context.Task.AddAsync(doTask);
            await _context.SaveChangesAsync();
        }

        public async Task<ToDoItem?> GetToDoItemByIdAsync(int id)
        {
            return await _context.Task.FirstOrDefaultAsync(ToDoitem => ToDoitem.Id == id);
        }

        public async Task<IEnumerable<ToDoItem>> GetAllToDoItemsAsync()
        {
            return await _context.Task
                .OrderBy(x => x.DueDate)
                .ToListAsync();
        }

        public async Task UpdateToDoItemAsync(ToDoItem doTask)
        {
            var taskToUpdate = await GetToDoItemByIdAsync(doTask.Id);
            if (taskToUpdate != null)
            {
                taskToUpdate.Title = doTask.Title;
                taskToUpdate.IsCompleted = doTask.IsCompleted;
                taskToUpdate.Description = doTask.Description;
                taskToUpdate.DueDate = doTask.DueDate;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteToDoItemAsync(int id)
        {
            var taskToDelete = await GetToDoItemByIdAsync(id);
            if (taskToDelete != null)
            {
                _context.Task.Remove(taskToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ToDoItem>> GetToDoItemsByDateAsync(DateTime date)
        {
            return await _context.Task
                .Where(x => x.DueDate.Date == date.Date)
                .OrderBy(x => x.DueDate) 
                .ToListAsync();
        }
    }
}

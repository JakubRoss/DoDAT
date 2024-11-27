using DoDAT.Domain.Entities;
using DoDAT.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoDAT.Infrastructure.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ToDoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ToDoItem> CreateToDoItemAsync(ToDoItem doTask)
        {
            await _context.ToDoItems.AddAsync(doTask);
            await _context.SaveChangesAsync();
            return doTask;
        }

        public async Task<ToDoItem?> ReadToDoItemByIdAsync(int id)
        {
            var toDoitem = await _context.ToDoItems.FirstOrDefaultAsync(ToDoitem => ToDoitem.Id == id);

            return toDoitem;
        }

        public async Task<IEnumerable<ToDoItem>> ReadAllToDoItemsAsync(int userId)
        {
            return await _context.ToDoItems.Where(toDoItem => toDoItem.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ToDoItem>> ReadToDoItemsByDateAsync(DateTime date, int userId)
        {
            return await _context.ToDoItems
                .Where(toDoItem => toDoItem.DueDate.Date == date.Date && toDoItem.UserId == userId)
                .OrderBy(toDoItem => toDoItem.DueDate)
                .ToListAsync();
        }

        public async Task<ToDoItem> UpdateToDoItemAsync(ToDoItem doTask, ToDoItem taskToUpdate)
        {
            if (taskToUpdate != null)
            {
                taskToUpdate.Title = doTask.Title;
                taskToUpdate.IsCompleted = doTask.IsCompleted;
                taskToUpdate.Description = doTask.Description;
                taskToUpdate.DueDate = doTask.DueDate;

                await _context.SaveChangesAsync();

                return taskToUpdate;
            }
            else
            {
                throw new InvalidOperationException("Task not found.");
            }
        }

        public async Task DeleteToDoItemAsync(ToDoItem taskToDelete)
        {
            _context.ToDoItems.Remove(taskToDelete);
            await _context.SaveChangesAsync();
        }
    }
}

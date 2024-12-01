using DoDAT.Application.Interfaces;
using DoDAT.Domain.Entities;
using DoDAT.Domain.Interfaces;
using DoDAT.Domain.Models;

namespace DoDAT.Application.Services
{
    public class ToDoitemService : IToDoitemService
    {
        private readonly IToDoItemRepository toDoItemRepository;

        public ToDoitemService(IToDoItemRepository toDoItemRepository)
        {
            this.toDoItemRepository = toDoItemRepository;
        }
        async Task<ToDoItem> IToDoitemService.AddToDoItemAsync(ToDoItemDto doTask, int userId)
        {
                return await toDoItemRepository.CreateToDoItemAsync(
                new ToDoItem
                {
                    Title = doTask.Title,
                    Description = doTask.Description,
                    IsCompleted = false,
                    DueDate = doTask.DueDate,
                    UserId = userId
                });
        }

        async Task IToDoitemService.DeleteToDoItemAsync(int id, int userId)
        {
            var itemToDelete = await toDoItemRepository.ReadToDoItemByIdAsync(id);
            if (itemToDelete == null || itemToDelete.UserId != userId)
                throw new UnauthorizedAccessException();
            await toDoItemRepository.DeleteToDoItemAsync(itemToDelete);
        }

        async Task<IEnumerable<ToDoItem>> IToDoitemService.GetAllToDoItemsAsync(int userId)
        {
            var toDoItems = await toDoItemRepository.ReadAllToDoItemsAsync(userId);
            return toDoItems;
        }

        async Task<ToDoItem> IToDoitemService.GetToDoItemByIdAsync(int toDoItemId, int userId)
        {
            var toDoItem = await toDoItemRepository.ReadToDoItemByIdAsync(userId);
            if (toDoItem == null || toDoItem.UserId != userId)
                throw new UnauthorizedAccessException();
            return toDoItem;
        }

        async Task<IEnumerable<ToDoItem>> IToDoitemService.GetToDoItemsByDateAsync(DateTime date, int userId)
        {
            var toDoItem = await toDoItemRepository.ReadToDoItemsByDateAsync(date, userId);
            return toDoItem;
        }

        async Task IToDoitemService.UpdateToDoItemAsync(ToDoItemDto payload, int userId, int taskId)
        {
            var taskToUpdate = await toDoItemRepository.ReadToDoItemByIdAsync(taskId);
            if (taskToUpdate == null || taskToUpdate.UserId != userId)
                throw new UnauthorizedAccessException();
            var tpayload = new ToDoItem
            {
                Title = payload.Title,
                Description = payload.Description,
                DueDate = payload.DueDate,
                IsCompleted = payload.IsCompleted
            };
            await toDoItemRepository.UpdateToDoItemAsync(tpayload, taskToUpdate);
        }
    }
}

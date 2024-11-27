using DoDAT.Domain.Entities;
using DoDAT.Domain.Models;

namespace DoDAT.Application.Interfaces
{
    /// <summary>
    /// Provides methods for managing ToDo items and user-specific operations.
    /// </summary>
    public interface IToDoitemService
    {
        /// <summary>
        /// Adds a new ToDo item for a specific user.
        /// </summary>
        /// <param name="doTask">The DTO containing the ToDo item details.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The created ToDo item.</returns>
        Task<ToDoItem> AddToDoItemAsync(ToDoItemDto doTask, int userId);

        /// <summary>
        /// Retrieves a specific ToDo item by ID and ensures it belongs to the user.
        /// </summary>
        /// <param name="toDoItemId">The ID of the ToDo item.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The requested ToDo item.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the item does not exist or does not belong to the user.</exception>
        Task<ToDoItem> GetToDoItemByIdAsync(int toDoItemId, int userId);

        /// <summary>
        /// Retrieves all ToDo items associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of the user's ToDo items.</returns>
        Task<IEnumerable<ToDoItem>> GetAllToDoItemsAsync(int userId);

        /// <summary>
        /// Updates a specific ToDo item, ensuring it belongs to the user.
        /// </summary>
        /// <param name="doTask">The updated ToDo item details.</param>
        /// <returns>A task representing the update operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the item does not exist or does not belong to the user.</exception>
        Task UpdateToDoItemAsync(ToDoItemDto payload, int userId, int taskId);

        /// <summary>
        /// Deletes a specific ToDo item, ensuring it belongs to the user.
        /// </summary>
        /// <param name="id">The ID of the ToDo item to delete.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A task representing the delete operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the item does not exist or does not belong to the user.</exception>
        Task DeleteToDoItemAsync(int id, int userId);

        /// <summary>
        /// Retrieves ToDo items for a user filtered by a specific date.
        /// </summary>
        /// <param name="date">The date to filter ToDo items.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A collection of the user's ToDo items filtered by date.</returns>
        Task<IEnumerable<ToDoItem>> GetToDoItemsByDateAsync(DateTime date, int userId);
    }
}
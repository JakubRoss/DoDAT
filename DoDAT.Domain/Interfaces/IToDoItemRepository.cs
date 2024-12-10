using DoDAT.Domain.Entities;

namespace DoDAT.Domain.Interfaces
{
    /// <summary>
    /// Interfejs definiujący operacje CRUD dla zadań ToDo.
    /// </summary>
    public interface IToDoItemRepository
    {
        /// <summary>
        /// Tworzy nowe zadanie ToDo i zapisuje je w bazie danych.
        /// </summary>
        /// <param name="doTask">Obiekt reprezentujący zadanie do dodania.</param>
        /// <returns>Zwraca obiekt zadania ToDo z uzupełnionym identyfikatorem (Id) nadanym przez bazę danych.</returns>
        Task<ToDoItem> CreateToDoItemAsync(ToDoItem doTask);

        /// <summary>
        /// Odczytuje zadanie ToDo na podstawie jego identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator zadania.</param>
        /// <returns>Zwraca obiekt zadania lub null, jeśli nie znaleziono.</returns>
        Task<ToDoItem?> ReadToDoItemByIdAsync(int id);

        /// <summary>
        /// Odczytuje wszystkie zadania ToDo przypisane do użytkownika.
        /// </summary>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// <returns>Zwraca listę zadań.</returns>
        Task<IEnumerable<ToDoItem>> ReadAllToDoItemsAsync(int userId);

        /// <summary>
        /// Odczytuje zadania ToDo przypisane do użytkownika na określoną datę.
        /// </summary>
        /// <param name="date">Data, dla której mają być odczytane zadania.</param>
        /// <param name="userId">Identyfikator użytkownika.</param>
        /// <returns>Zwraca listę zadań spełniających warunki.</returns>
        Task<IEnumerable<ToDoItem>> ReadToDoItemsByDateAsync(DateOnly date, int userId);

        /// <summary>
        /// Updates the properties of an existing ToDo item with the values from another ToDo item.
        /// </summary>
        /// <param name="doTask">The ToDo item containing updated values.</param>
        /// <param name="taskToUpdate">The existing ToDo item to be updated.</param>
        /// <returns>The updated ToDo item.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the task to update is not found.</exception>
        Task<ToDoItem> UpdateToDoItemAsync(ToDoItem doTask, ToDoItem taskToUpdate);

        /// <summary>
        /// Deletes the specified ToDo item from the database.
        /// </summary>
        /// <param name="taskToDelete">The ToDo item to be deleted.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task DeleteToDoItemAsync(ToDoItem taskToDelete);
    }
}
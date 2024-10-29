namespace DoDAT.Presentation.Domain
{
    public interface IToDoitemRepository
    {
        /// <summary>
        /// Dodaje nowe zadanie do bazy danych.
        /// </summary>
        /// <param name="doTask">Model zadania do dodania.</param>
        Task AddToDoItemAsync(ToDoItem doTask);

        /// <summary>
        /// Pobiera zadanie po identyfikatorze.
        /// </summary>
        /// <param name="id">Identyfikator zadania.</param>
        /// <returns>Model zadania lub null, jeśli nie znaleziono.</returns>
        Task<ToDoItem?> GetToDoItemByIdAsync(int id);

        /// <summary>
        /// Pobiera wszystkie zadania z bazy danych.
        /// </summary>
        /// <returns>Lista zadań.</returns>
        Task<IEnumerable<ToDoItem>> GetAllToDoItemsAsync();

        /// <summary>
        /// Aktualizuje istniejące zadanie.
        /// </summary>
        /// <param name="doTask">Model zadania do aktualizacji.</param>
        Task UpdateToDoItemAsync(ToDoItem doTask);

        /// <summary>W
        /// Usuwa zadanie na podstawie identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator zadania do usunięcia.</param>
        Task DeleteToDoItemAsync(int id);

        /// <summary>
        /// Pobiera zadania dla określonej daty
        /// </summary>
        /// <param name="date">Data, według której mają zostać przefiltrowane zadania ToDo.</param>
        Task<IEnumerable<ToDoItem>> GetToDoItemsByDateAsync(DateTime date);
    }
}
using System.Linq.Expressions;
using DoDAT.Domain.Entities;

namespace DoDAT.Domain.Interfaces
{
    /// <summary>
    /// Interfejs definiujący operacje CRUD dla użytkowników.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Tworzy nowego użytkownika.
        /// </summary>
        /// <param name="user">Obiekt użytkownika do utworzenia.</param>
        /// <returns>Task reprezentujący operację asynchroniczną.</returns>
        Task CreateUserAsync(User user);

        /// <summary>
        /// Wyszukuje użytkownika na podstawie zadanego kryterium.
        /// </summary>
        /// <param name="predicate">
        /// Wyrażenie lambda określające kryterium wyszukiwania, np. user => user.Id == 1.
        /// </param>
        /// <returns>Obiekt użytkownika, jeśli znaleziono; w przeciwnym razie null.</returns>
        Task<User?> ReadUserAsync(Expression<Func<User, bool>> predicate);

        /// <summary>
        /// Aktualizuje dane istniejącego użytkownika.
        /// </summary>
        /// <param name="user">Obiekt użytkownika z nowymi danymi.</param>
        /// <returns>Zaktualizowany obiekt użytkownika.</returns>
        Task<User> UpdateUserAsync(User user);

        /// <summary>
        /// Usuwa użytkownika na podstawie identyfikatora.
        /// </summary>
        /// <param name="id">Identyfikator użytkownika do usunięcia.</param>
        /// <returns>Task reprezentujący operację asynchroniczną.</returns>
        Task DeleteUserAsync(int id);
    }
}
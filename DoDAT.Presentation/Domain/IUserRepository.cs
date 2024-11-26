using DoDAT.Presentation.Controllers;
using DoDAT.Presentation.Infrastructure;

namespace DoDAT.Presentation.Domain
{
    public interface IUserRepository
    {
        ApplicationDbContext _context { get; }

        Task<User> LogIn(UserDto userDto);
        Task Register(UserDto userDto);
    }
}
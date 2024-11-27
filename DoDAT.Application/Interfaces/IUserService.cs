using DoDAT.Domain.Entities;
using DoDAT.Domain.Models;

namespace DoDAT.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> LogIn(UserDto userDto);
        Task Register(UserDto userDto);
    }
}
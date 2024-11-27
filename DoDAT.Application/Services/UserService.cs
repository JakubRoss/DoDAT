using DoDAT.Application.Interfaces;
using DoDAT.Domain.Entities;
using DoDAT.Domain.Interfaces;
using DoDAT.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace DoDAT.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher<User> passwordHasher;

        public UserService(IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }
        public async Task<User> LogIn(UserDto userDto)
        {
            var user = await userRepository.ReadUserAsync(u => u.Login.ToLower() == userDto.Login.ToLower());
            if (user == null)
                throw new UnauthorizedAccessException("Invalid User name or password");

            var result = passwordHasher.VerifyHashedPassword(user, user.passwordHash, userDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new InvalidOperationException("Invalid User name or password");

            return user;
        }

        public async Task Register(UserDto userDto)
        {
            var user = await userRepository.ReadUserAsync(u => u.Login.ToLower() == userDto.Login.ToLower());
            if (user != null)
                throw new InvalidOperationException("Invalid User name or password");
            user = new User();
            await userRepository.CreateUserAsync(
                new User
                {
                    Login = userDto.Login,
                    passwordHash = passwordHasher.HashPassword(user, userDto.Password)
                });
        }
    }
}

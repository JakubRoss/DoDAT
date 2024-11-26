using DoDAT.Presentation.Controllers;
using DoDAT.Presentation.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DoDAT.Presentation.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly IPasswordHasher<Domain.User> _passwordHasher;

        public UserRepository(ApplicationDbContext context, IPasswordHasher<Domain.User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public ApplicationDbContext _context { get; }

        public async Task Register(UserDto userDto)
        {
            var dataBaseUser = await _context.User
                .SingleOrDefaultAsync(u => u.Login.ToLower() == userDto.Login.ToLower());
            if (userDto == null || dataBaseUser != null)
                throw new InvalidOperationException("Invalid login");

            var user = new Domain.User { Login = userDto.Login };
            var hashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
            user.passwordHash = hashedPassword;


            await _context.User.AddAsync(new Domain.User { Login = userDto.Login, passwordHash = hashedPassword });
            await _context.SaveChangesAsync();
        }
        public async Task<Domain.User> LogIn(UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Login) || string.IsNullOrWhiteSpace(userDto.Password))
                throw new InvalidOperationException("Invalid User name or password");

            var user = await _context.User
                .SingleOrDefaultAsync(u => u.Login.ToLower() == userDto.Login.ToLower());
            if (user == null)
                throw new InvalidOperationException("Invalid User name or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.passwordHash, userDto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new InvalidOperationException("Invalid User name or password");

            return user;
        }
    }
}

using DoDAT.Domain.Entities;
using DoDAT.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DoDAT.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDbContext _context { get; }

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> ReadUserAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.FirstOrDefaultAsync(predicate);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var userDb = await ReadUserAsync(x => x.Id == user.Id);
            if (userDb != null)
            {
                userDb.Login = userDb.Login;
                userDb.passwordHash = userDb.passwordHash;
                await _context.SaveChangesAsync();

                return userDb;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await ReadUserAsync(x => x.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}

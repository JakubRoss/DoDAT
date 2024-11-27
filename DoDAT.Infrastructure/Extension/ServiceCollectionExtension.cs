using DoDAT.Domain.Interfaces;
using DoDAT.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DoDAT.Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection service)
        {
            // Dodaj DbContext z SQLite
            service.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=tasks.db"));

            service.AddScoped<DatabaseSeeder>();

            //Rejestracja Serwisow
            service.AddScoped<IToDoItemRepository, ToDoItemRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
        }
    }
}

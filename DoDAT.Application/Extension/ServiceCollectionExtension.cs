using DoDAT.Application.Interfaces;
using DoDAT.Application.MIddleware;
using DoDAT.Application.Services;
using DoDAT.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DoDAT.Application.Extension
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection service)
        {
            //Rejestracja Serwisow
            service.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            service.AddTransient<ErrorHandlingMiddleware>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IToDoitemService, ToDoitemService>(); 
        }
    }
}

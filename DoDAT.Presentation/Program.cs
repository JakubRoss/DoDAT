using DoDAT.Application.Extension;
using DoDAT.Application.MIddleware;
using DoDAT.Infrastructure;
using DoDAT.Infrastructure.Extension;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DoDAT.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddInfrastructure();
            builder.Services.AddApplication();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = false; // Cookie dostepne tylko przez HTTP (nie JS | if true) zrobic sprawdzanie po stronie serwera
                    options.Cookie.SecurePolicy = builder.Environment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always; // Tylko HTTPS - Always
                    if (!builder.Environment.IsDevelopment())
                    {
                        options.Cookie.SameSite = SameSiteMode.None; // Chroni przed atakami CSRF - Lax
                    }
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Czas trwania sesji
                    options.SlidingExpiration = true; // Odnawianie ciasteczka
                    options.LoginPath = "/api/account/login"; // Endpoint logowania
                    options.LogoutPath = "/api/account/logout"; // Endpoint wylogowania
                });

            // Rejestracja autoryzacji
            builder.Services.AddAuthorization();

            //Rejestracja Serwisow


            var app = builder.Build();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
            seeder.Seed();

            if(!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

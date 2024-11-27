using DoDAT.Presentation.Domain;
using DoDAT.Presentation.Infrastructure;
using DoDAT.Presentation.MIddleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DoDAT.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Dodaj DbContext z SQLite
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=tasks.db"));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = false; // Cookie dostepne tylko przez HTTP (nie JS) zrobic sprawdzanie po stronie serwera
                    options.Cookie.SecurePolicy = builder.Environment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always; // Tylko HTTPS - Always
                    if (!builder.Environment.IsDevelopment())
                    {
                        options.Cookie.SameSite = SameSiteMode.None; // Chroni przed atakami CSRF - Lax
                    }
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Czas trwania sesji
                    options.SlidingExpiration = true; // Odnawianie ciasteczka
                    options.LoginPath = "/account/login"; // Endpoint logowania
                    options.LogoutPath = "/account/logout"; // Endpoint wylogowania
                });


            //Rejestracja Serwisow
            builder.Services.AddScoped<IToDoitemRepository,ToDoitemRepository>();
            builder.Services.AddTransient<DatabaseSeeder>();
            builder.Services.AddTransient<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
            seeder.Seed();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

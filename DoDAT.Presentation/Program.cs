using DoDAT.Application.Extension;
using DoDAT.Application.MIddleware;
using DoDAT.Infrastructure;
using DoDAT.Infrastructure.Extension;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;

namespace DoDAT.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Dodanie SwaggerGen
            #region
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "DoDAT API",
                    Version = "v1",
                    //Description = "API z uwierzytelnianiem do obslugi ToDo List"
                    Contact = new OpenApiContact
                    {
                        Name = "4q4b",
                        Email = "jakub.ross@gitify.net",
                    }
                });

                options.AddSecurityDefinition("CookieAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Cookie",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Cookie,
                    Description = "Uwierzytelnianie na podstawie ciasteczek"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "CookieAuth"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            #endregion

            // Dodaj us³ugê CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowVuet", policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // Podaj dok³adn¹ domenê frontendow¹
                          .AllowCredentials() // Pozwól na poœwiadczenia (np. ciasteczka)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

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

            var app = builder.Build();

            app.UseSwagger(); // Dodaje endpoint do specyfikacji JSON (/swagger/v1/swagger.json)
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DoDAT API v1");
                c.RoutePrefix = string.Empty; // Ustawienie Swaggera na glowna sciezke
            });

            // U¿yj CORS
            app.UseCors("AllowVuet");

            app.UseMiddleware<ErrorHandlingMiddleware>();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
            seeder.Seed();

            if(!app.Environment.IsDevelopment())
            {
                //app.UseHttpsRedirection();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

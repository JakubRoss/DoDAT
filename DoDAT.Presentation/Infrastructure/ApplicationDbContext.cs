using DoDAT.Presentation.Domain;
using Microsoft.EntityFrameworkCore;

namespace DoDAT.Presentation.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<ToDoItem> Task { get; set; }
    }
}

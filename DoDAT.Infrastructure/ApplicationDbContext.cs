using DoDAT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoDAT.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {

        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasMany(u => u.Items)             
                .WithOne(t => t.User)              
                .HasForeignKey(t => t.UserId)      
                .IsRequired()                      
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ToDoItem>()
                .Property(e => e.DueDate)
                .HasConversion(
                    v => v.ToString("yyyy-MM-dd"),  // Konwersja DateOnly -> string
                    v => DateOnly.Parse(v) // Konwersja string -> DateOnly
                );

            base.OnModelCreating(modelBuilder);
        }

    }
}

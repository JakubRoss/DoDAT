using Microsoft.EntityFrameworkCore;

namespace DoDAT.Presentation.Infrastructure
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        public DatabaseSeeder(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {

            if (_dbContext.Database.CanConnect())
            {
                var pendingMigration = _dbContext.Database.GetPendingMigrations();
                if (pendingMigration != null && pendingMigration.Any())
                {
                    _dbContext.Database.Migrate();
                }
            }

        }

    }
}
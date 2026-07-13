using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Persistence
{
    public static class DatabaseExtensions
    {
        public static async Task MigrateDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context =
                scope.ServiceProvider
                     .GetRequiredService<ClassifiedAdDbContext>();

            await context.Database.MigrateAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMarketplacePersistence(
            this IServiceCollection services,
            string connectionString,
            bool enableSensitiveLogging = false)
        {
            services.AddDbContext<ClassifiedAdDbContext>(options =>
            {
                options.UseNpgsql(connectionString);

#if DEBUG
                if (enableSensitiveLogging)
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
#endif
            });

            return services;
        }
    }
}
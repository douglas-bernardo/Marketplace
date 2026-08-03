using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Marketplace.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMarketplacePersistence(
            this IServiceCollection services,
            string connectionString,
            bool enableSensitiveLogging = false)
        {
            services.AddDbContext<MarketplaceDbContext>(options =>
            {
                options.UseNpgsql(connectionString);

                // Suprimir warning sobre optional dependents sem coluna identificadora
                // Este é um comportamento esperado para value objects opcionais em DDD
                options.ConfigureWarnings(warnings =>
                {
                    warnings.Ignore(RelationalEventId.OptionalDependentWithoutIdentifyingPropertyWarning);
                });

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
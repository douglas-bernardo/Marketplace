using Marketplace.Domain;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Persistence;

public sealed class ClassifiedAdDbContext : DbContext
{
    public ClassifiedAdDbContext(DbContextOptions<ClassifiedAdDbContext> options)
        : base(options)
    {
    }

    public DbSet<ClassifiedAd> ClassifiedAds => Set<ClassifiedAd>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all configurations from the current assembly. This approach avoids the need to manually register each entity configuration class.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClassifiedAdDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
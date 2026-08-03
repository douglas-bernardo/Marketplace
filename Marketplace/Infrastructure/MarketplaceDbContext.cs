using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Persistence;

public sealed class MarketplaceDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public MarketplaceDbContext(
        DbContextOptions<MarketplaceDbContext> options,
        ILoggerFactory loggerFactory)
        : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    public DbSet<Domain.ClassifiedAd.ClassifiedAd> ClassifiedAds
        => Set<Domain.ClassifiedAd.ClassifiedAd>();
    public DbSet<Domain.UserProfile.UserProfile> UserProfiles
        => Set<Domain.UserProfile.UserProfile>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all configurations from the current assembly. This approach avoids the need to manually register each entity configuration class.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarketplaceDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
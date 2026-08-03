using Marketplace.Domain.ClassifiedAd;
using Marketplace.Infrastructure.Persistence;

namespace Marketplace.ClassifiedAd
{
    public class ClassifiedAdRepository : IClassifiedAdRepository, IDisposable
    {
        private readonly MarketplaceDbContext _dbContext;

        public ClassifiedAdRepository(MarketplaceDbContext dbContext)
            => _dbContext = dbContext;

        public async Task Add(Domain.ClassifiedAd.ClassifiedAd entity)
            => await _dbContext.ClassifiedAds.AddAsync(entity);

        public async Task<bool> Exists(ClassifiedAdId id)
            => await _dbContext.ClassifiedAds.FindAsync(id.Value) != null;

        public async Task<Domain.ClassifiedAd.ClassifiedAd> Load(ClassifiedAdId id)
            => await _dbContext.ClassifiedAds.FindAsync(id.Value)
                ?? throw new InvalidOperationException($"ClassifiedAd with id {id} not found.");

        public void Dispose() => _dbContext.Dispose();
    }
}

using Marketplace.Domain.ClassifiedAd;
using Marketplace.Infrastructure.Persistence;

namespace Marketplace.Infrastructure
{
    public class ClassifiedAdRepository : IClassifiedAdRepository
    {
        private readonly ClassifiedAdDbContext _dbContext;

        public ClassifiedAdRepository(ClassifiedAdDbContext dbContext)
            => _dbContext = dbContext;

        public async Task Add(ClassifiedAd entity)
            => await _dbContext.ClassifiedAds.AddAsync(entity);

        public async Task<bool> Exists(ClassifiedAdId id)
            => await _dbContext.ClassifiedAds.FindAsync(id.Value) != null;

        public async Task<ClassifiedAd> Load(ClassifiedAdId id)
            => await _dbContext.ClassifiedAds.FindAsync(id.Value)
                ?? throw new InvalidOperationException($"ClassifiedAd with id {id} not found.");
    }
}

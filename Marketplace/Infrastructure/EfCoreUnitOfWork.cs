using Marketplace.Framework;
using Marketplace.Infrastructure.Persistence;

namespace Marketplace.Infrastructure
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly MarketplaceDbContext _dbContext;

        public EfCoreUnitOfWork(MarketplaceDbContext dbContext)
            => _dbContext = dbContext;

        public Task Commit() => _dbContext.SaveChangesAsync();
    }
}

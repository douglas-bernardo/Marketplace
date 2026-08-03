using Marketplace.Domain.Shared;
using Marketplace.Domain.UserProfile;
using Marketplace.Infrastructure.Persistence;

namespace Marketplace.UserProfile
{
    public class UserProfileRepository : IUserProfileRepository, IDisposable
    {
        private readonly MarketplaceDbContext _dbContext;
        public UserProfileRepository(MarketplaceDbContext dbContext)
            => _dbContext = dbContext;
        public async Task Add(Domain.UserProfile.UserProfile entity)
            => await _dbContext.UserProfiles.AddAsync(entity);
        public async Task<bool> Exists(UserId id)
            => await _dbContext.UserProfiles.FindAsync(id.Value) != null;
        public async Task<Domain.UserProfile.UserProfile> Load(UserId id)
            => await _dbContext.UserProfiles.FindAsync(id.Value)
                ?? throw new InvalidOperationException($"UserProfile with id {id} not found.");

        public void Dispose() => _dbContext.Dispose();
    }
}

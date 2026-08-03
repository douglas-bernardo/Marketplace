using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Persistence.Configurations
{
    public class UserProfileEntityTypeConfiguration 
        : IEntityTypeConfiguration<Domain.UserProfile.UserProfile>
    {
        public void Configure(EntityTypeBuilder<Domain.UserProfile.UserProfile> builder)
        {
            builder.HasKey(x => x.UserProfileId);
            builder.OwnsOne(x => x.Id);
            builder.OwnsOne(x => x.DisplayName);
            builder.OwnsOne(x => x.FullName);
        }
    }
}

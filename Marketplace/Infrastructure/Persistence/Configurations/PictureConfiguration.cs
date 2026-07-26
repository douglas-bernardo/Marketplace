using Marketplace.Domain.ClassifiedAd;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Persistence.Configurations
{
    public sealed class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(x => x.PictureId);
            builder.OwnsOne(x => x.Id);
            builder.OwnsOne(x => x.ParentId);
            builder.OwnsOne(x => x.Size);
        }
    }
}

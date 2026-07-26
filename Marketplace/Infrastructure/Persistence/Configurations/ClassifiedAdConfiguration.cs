using Marketplace.Domain.ClassifiedAd;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Persistence.Configurations
{
    public sealed class ClassifiedAdConfiguration
    : IEntityTypeConfiguration<ClassifiedAd>
    {
        public void Configure(EntityTypeBuilder<ClassifiedAd> builder)
        {
            builder.HasKey(x => x.ClassifiedAdId);
            builder.OwnsOne(x => x.Id);

            // Configure Price as optional - configure the owned entity first, then the navigation
            builder.OwnsOne(x => x.Price, price =>
            {
                price.OwnsOne(x => x.Currency);
            });
            builder.Navigation(x => x.Price).IsRequired(false);

            // Configure Text as optional
            builder.OwnsOne(x => x.Text, text =>
            {
                text.Property(x => x.Value).IsRequired(false);
            });
            builder.Navigation(x => x.Text).IsRequired(false);

            // Configure Title as optional
            builder.OwnsOne(x => x.Title, title =>
            {
                title.Property(x => x.Value).IsRequired(false);
            });
            builder.Navigation(x => x.Title).IsRequired(false);

            builder.OwnsOne(x => x.ApprovedBy);
            builder.OwnsOne(x => x.OwnerId);
        }
    }
}

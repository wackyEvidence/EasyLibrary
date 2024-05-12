using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLibrary.DataAccess.Configurations
{
    internal class PublishingHouseConfiguration : IEntityTypeConfiguration<PublishingHouseEntity>
    {
        public void Configure(EntityTypeBuilder<PublishingHouseEntity> builder)
        {
            builder.HasKey(ph => ph.Id);
            builder.Property(ph => ph.Name).IsRequired().HasColumnType("nvarchar(50)");
            // Relations
            builder.HasMany(ph => ph.BookTypes).WithOne(bt => bt.PublishingHouse).HasForeignKey(bt => bt.PublishingHouseId).HasPrincipalKey(ph => ph.Id);
            // Indexes
            builder.HasIndex(ph => ph.Name).IsUnique();
        }
    }
}

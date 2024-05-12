using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLibrary.DataAccess.Configurations
{
    internal class BookTypeConfiguration : IEntityTypeConfiguration<BookTypeEntity>
    {
        public void Configure(EntityTypeBuilder<BookTypeEntity> builder)
        {
            builder.HasKey(bt => bt.Id);
            builder.Property(bt => bt.Title).IsRequired().HasColumnType("varchar(50)");
            builder.Property(bt => bt.Binding).HasConversion<int>().IsRequired();
            builder.Property(bt => bt.PublishingYear).IsRequired();
            builder.Property(bt => bt.ISBN).IsRequired().HasColumnType("varchar(17)");
            builder.Property(bt => bt.PublishingYear).IsRequired();
            builder.Property(bt => bt.Weight).IsRequired(); 
            builder.Property(bt => bt.AvailableForIssuance).IsRequired();
            builder.Property(bt => bt.TimesIssued).IsRequired();
            builder.Property(bt => bt.AppearanceDate).IsRequired().HasColumnType("date").HasDefaultValue(DateOnly.Parse(DateTime.Now.ToShortDateString()));
            builder.Property(bt => bt.MinAge).IsRequired();
            // Indexes 
            builder.HasIndex(bt => bt.Title);
            builder.HasIndex(bt => bt.ISBN);
            builder.HasIndex(bt => bt.SeriesId);
            builder.HasIndex(bt => bt.PublishingHouseId);
        }
    }
}

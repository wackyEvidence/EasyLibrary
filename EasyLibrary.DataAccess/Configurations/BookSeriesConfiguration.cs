using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLibrary.DataAccess.Configurations
{
    internal class BookSeriesConfiguration : IEntityTypeConfiguration<BookSeriesEntity>
    {
        public void Configure(EntityTypeBuilder<BookSeriesEntity> builder)
        {
            builder.HasKey(bs => bs.Id); 
            builder.Property(bs => bs.Name).IsRequired().HasColumnName("nvarchar(50)");
            builder.HasMany(bs => bs.BookTypes).WithOne(bt => bt.Series).HasForeignKey(bt => bt.SeriesId).HasPrincipalKey(bs => bs.Id);
        }
    }
}

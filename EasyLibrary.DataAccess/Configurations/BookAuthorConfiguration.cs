using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLibrary.DataAccess.Configurations
{
    internal class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthorEntity>
    {
        public void Configure(EntityTypeBuilder<BookAuthorEntity> builder)
        {
            builder.HasKey(ba => ba.Id);
            builder.Property(ba => ba.Name).IsRequired().HasColumnType("varchar(100)");
            builder.Property(ba => ba.Bio).HasColumnType("text");
            // Relations 
            builder.HasMany(ba => ba.BookTypes).WithMany(bt => bt.Authors);
            // Indexes
            builder.HasIndex(ba => ba.Name).IsUnique();
        }
    }
}

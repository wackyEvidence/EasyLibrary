using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EasyLibrary.Core;

namespace EasyLibrary.DataAccess.Configurations
{
    internal class BookCopyConfiguration : IEntityTypeConfiguration<BookCopyEntity>
    {
        public void Configure(EntityTypeBuilder<BookCopyEntity> builder)
        {
            builder.HasKey(bc => bc.Id);
            // TODO попробовать вставить inventoryNumber с длиной не 10 символов
            builder.Property(bc => bc.InventoryNumber).IsRequired().HasColumnType("varchar(10)");
            builder.Property(bc => bc.Status).IsRequired().HasConversion<int>().HasDefaultValue(BookStatus.InStock);
            // Relations 
            builder.HasOne(bc => bc.Type).WithMany(bt => bt.Copies).HasForeignKey(bc => bc.TypeId).HasPrincipalKey(bt => bt.Id);
            // Indexes
            builder.HasIndex(bc => bc.InventoryNumber).IsUnique();
        }
    }
}

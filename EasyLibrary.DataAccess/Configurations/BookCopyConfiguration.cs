using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EasyLibrary.Core;
using EasyLibrary.Core.Models;

namespace EasyLibrary.DataAccess.Configurations
{
    internal class BookCopyConfiguration : IEntityTypeConfiguration<BookCopyEntity>
    {
        public void Configure(EntityTypeBuilder<BookCopyEntity> builder)
        {
            builder.HasKey(bc => bc.Id);
            builder.Property(bc => bc.InventoryNumber).IsRequired().HasColumnType($"varchar({BookCopy.INVENTORY_NUMBER_LENGTH})");
            builder.Property(bc => bc.Status).IsRequired().HasConversion<int>().HasDefaultValue(BookStatus.InStock);
            // Relations 
            builder.HasOne(bc => bc.Type).WithMany(bt => bt.Copies).HasForeignKey(bc => bc.TypeId).HasPrincipalKey(bt => bt.Id);
            // Indexes
            builder.HasIndex(bc => bc.InventoryNumber).IsUnique();
        }
    }
}

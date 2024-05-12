﻿using EasyLibrary.DataAccess.Entites;
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
            builder.HasOne(bc => bc.Type).WithMany(bt => bt.Copies).HasForeignKey(bc => bc.TypeId).HasPrincipalKey(bt => bt.Id);
            // TODO попробовать вставить inventoryNumber с длиной не 10 символов
            builder.Property(bc => bc.InventoryNumber).IsRequired().HasColumnType("nvarchar(10)").IsFixedLength();
            builder.Property(bc => bc.Status).IsRequired().HasConversion<int>().HasDefaultValue(BookStatus.InStock);
        }
    }
}

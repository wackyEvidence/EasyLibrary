using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLibrary.DataAccess.Configurations
{
    internal class BookIssuanceConfiguration : IEntityTypeConfiguration<BookIssuanceEntity>
    {
        public void Configure(EntityTypeBuilder<BookIssuanceEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.IssuanceDate).IsRequired();
            builder.Property(e => e.IsFinished).IsRequired();   
            // Relations
            builder.HasOne(e => e.BookCopy).WithMany(bc => bc.BookIssuances).HasForeignKey(e => e.BookCopyId).HasPrincipalKey(bc => bc.Id);
            builder.HasOne(e => e.User).WithMany(u => u.BookIssuances).HasForeignKey(e => e.UserId).HasPrincipalKey(u => u.Id);

            builder.ToTable("BookIssuances");
        }
    }
}

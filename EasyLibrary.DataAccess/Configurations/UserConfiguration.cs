using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLibrary.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name).IsRequired().HasColumnType("varchar(50)");
            builder.Property(u => u.Surname).HasColumnType("varchar(50)");
            builder.Property(u => u.Patronymic).HasColumnType("varchar(50)");
            builder.Property(u => u.PassportSeries).HasColumnType($"varchar({User.PASSPORT_SERIES_LENGTH})");
            builder.Property(u => u.PassportNumber).HasColumnType($"varchar({User.PASSPORT_NUMBER_LENGTH})");
            builder.Property(u => u.BirthDate).HasColumnType("date");
            builder.Property(u => u.RegistrationDate).IsRequired().HasColumnType("date"); 
            builder.Property(u => u.Email).HasColumnType("varchar(50)").IsRequired();
            builder.Property(u => u.PasswordHash).HasColumnType("varchar(60)").IsRequired();
            builder.Property(u => u.PhoneNumber).HasColumnType($"varchar({User.PHONE_NUMBER_LENGTH})");
            builder.Property(u => u.IsAdmin).IsRequired().HasDefaultValue(false);
            // Indexes 
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}

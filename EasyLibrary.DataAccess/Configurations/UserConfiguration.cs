﻿using EasyLibrary.DataAccess.Entites;
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
            builder.Property(u => u.Surname).IsRequired().HasColumnType("varchar(50)");
            builder.Property(u => u.Patronymic).HasColumnType("varchar(50)");
            builder.Property(u => u.PassportSeries).IsRequired().HasColumnType("varchar(4)");
            builder.Property(u => u.PassportNumber).IsRequired().HasColumnType("varchar(6)");
            builder.Property(u => u.BirthDate).IsRequired().HasColumnType("date");
            builder.Property(u => u.RegistrationDate).IsRequired().HasColumnType("date"); 
            builder.Property(u => u.Email).HasColumnType("varchar(50)").IsRequired();
            builder.Property(u => u.PhoneNumber).IsRequired().HasColumnType("varchar(16)");
            builder.Property(u => u.IsAdmin).IsRequired().HasDefaultValue(false);
            // Indexes 
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}

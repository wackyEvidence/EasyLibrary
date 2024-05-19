﻿// <auto-generated />
using System;
using EasyLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EasyLibrary.DataAccess.Migrations
{
    [DbContext(typeof(EasyLibraryDbContext))]
    [Migration("20240512174014_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookAuthorEntityBookTypeEntity", b =>
                {
                    b.Property<Guid>("AuthorsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookTypesId")
                        .HasColumnType("uuid");

                    b.HasKey("AuthorsId", "BookTypesId");

                    b.HasIndex("BookTypesId");

                    b.ToTable("BookAuthorEntityBookTypeEntity");
                });

            modelBuilder.Entity("EasyLibrary.DataAccess.Entites.BookAuthorEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("BookAuthorEntity");
                });

            modelBuilder.Entity("EasyLibrary.DataAccess.Entites.BookCopyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("InventoryNumber")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("InventoryNumber")
                        .IsUnique();

                    b.HasIndex("TypeId");

                    b.ToTable("BookCopies");
                });

            modelBuilder.Entity("EasyLibrary.DataAccess.Entites.BookSeriesEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("BookSeriesEntity");
                });

            modelBuilder.Entity("EasyLibrary.DataAccess.Entites.BookTypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("AppearanceDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValue(new DateOnly(2024, 5, 12));

                    b.Property<bool>("AvailableForIssuance")
                        .HasColumnType("boolean");

                    b.Property<int>("Binding")
                        .HasColumnType("integer");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("varchar(17)");

                    b.Property<int>("MinAge")
                        .HasColumnType("integer");

                    b.Property<int>("PagesCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("PublishingHouseId")
                        .HasColumnType("uuid");

                    b.Property<int>("PublishingYear")
                        .HasColumnType("integer");

                    b.Property<Guid>("SeriesId")
                        .HasColumnType("uuid");

                    b.Property<int>("TimesIssued")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ISBN");

                    b.HasIndex("PublishingHouseId");

                    b.HasIndex("SeriesId");

                    b.HasIndex("Title");

                    b.ToTable("BookTypes");
                });

            modelBuilder.Entity("EasyLibrary.DataAccess.Entites.PublishingHouseEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PublishingHouseEntity");
                });

            modelBuilder.Entity("EasyLibrary.DataAccess.Entites.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasColumnType("varchar(6)");

                    b.Property<string>("PassportSeries")
                        .IsRequired()
                        .HasColumnType("varchar(4)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(16)");

                    b.Property<DateOnly>("RegistrationDate")
                        .HasColumnType("date");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BookAuthorEntityBookTypeEntity", b =>
                {
                    b.HasOne("EasyLibrary.DataAccess.Entites.BookAuthorEntity", null)
                        .WithMany()
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLibrary.DataAccess.Entites.BookTypeEntity", null)
                        .WithMany()
                        .HasForeignKey("BookTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EasyLibrary.DataAccess.Entites.BookCopyEntity", b =>
                {
                    b.HasOne("EasyLibrary.DataAccess.Entites.BookTypeEntity", "Type")
                        .WithMany("Copies")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("EasyLibrary.DataAccess.Entites.BookTypeEntity", b =>
                {
                    b.HasOne("EasyLibrary.DataAccess.Entites.PublishingHouseEntity", "PublishingHouse")
                        .WithMany("BookTypes")
                        .HasForeignKey("PublishingHouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyLibrary.DataAccess.Entites.BookSeriesEntity", "Series")
                        .WithMany("BookTypes")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PublishingHouse");

                    b.Navigation("Series");
                });

            modelBuilder.Entity("EasyLibrary.DataAccess.Entites.BookSeriesEntity", b =>
                {
                    b.Navigation("BookTypes");
                });

            modelBuilder.Entity("EasyLibrary.DataAccess.Entites.BookTypeEntity", b =>
                {
                    b.Navigation("Copies");
                });

            modelBuilder.Entity("EasyLibrary.DataAccess.Entites.PublishingHouseEntity", b =>
                {
                    b.Navigation("BookTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
using EasyLibrary.Core.Models;
using EasyLibrary.DataAccess.Configurations;
using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace EasyLibrary.DataAccess
{
    public class EasyLibraryDbContext : DbContext
    {
        public EasyLibraryDbContext(DbContextOptions<EasyLibraryDbContext> options) : base(options) 
        {
            
        }

        public DbSet<BookAuthorEntity> BookAuthorEntity { get; set; }
        public DbSet<BookCopyEntity> BookCopies { get; set; }
        public DbSet<BookSeriesEntity> BookSeriesEntity { get; set; }
        public DbSet<BookTypeEntity> BookTypes { get; set; }
        public DbSet<PublishingHouseEntity> PublishingHouseEntity { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BookAuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookCopyConfiguration());
            modelBuilder.ApplyConfiguration(new BookSeriesConfiguration());
            modelBuilder.ApplyConfiguration(new BookTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PublishingHouseConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}

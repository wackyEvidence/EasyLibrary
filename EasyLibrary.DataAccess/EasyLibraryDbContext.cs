using EasyLibrary.DataAccess.Configurations;
using EasyLibrary.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace EasyLibrary.DataAccess
{
    public class EasyLibraryDbContext : DbContext
    {
        public EasyLibraryDbContext(DbContextOptions<EasyLibraryDbContext> options) : base(options) 
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<BookTypeEntity> BookTypes { get; set; }
        public DbSet<BookCopyEntity> BookCopies { get; set; }
        public DbSet<UserEntity> Users { get; set; }

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

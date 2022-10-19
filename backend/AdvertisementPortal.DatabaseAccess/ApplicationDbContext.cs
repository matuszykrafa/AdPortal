using AdvertisementPortal.Common.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementPortal.DatabaseAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserModel>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            builder.Entity<UserModel>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<OfferModel> Offers { get; set; }
        public DbSet<ImageDataModel> ImagesData { get; set; }
    }
}

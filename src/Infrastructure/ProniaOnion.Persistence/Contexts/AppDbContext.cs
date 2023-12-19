using Microsoft.EntityFrameworkCore;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Contexts
{
    public class AppDbContext:DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.Description).HasMaxLength(500);
            modelBuilder.Entity<Product>().Property(p => p.SKU).IsRequired().HasMaxLength(10);

            base.OnModelCreating(modelBuilder);
        }

    }
}



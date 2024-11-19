using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAPI_S1.Models;

namespace WebAPI_S1.Data
{
	public class ProductContext : DbContext
	{
		public virtual DbSet<Product>	Products { get; set; }
		public virtual DbSet<ProductGroup> Groups { get; set; }
		public virtual DbSet<Storage>	Storages { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder
				.LogTo(Console.WriteLine).UseLazyLoadingProxies()
				.UseNpgsql("Host=localhost;Username=postgres;Password=BorlandC++Builder6.0;Database=postgres");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductGroup>(entity =>
			{
				entity.HasKey(pg => pg.Id)
				.HasName("product_group_pk");
				
				entity.ToTable("Categories");
				
				entity.Property(pg => pg.Name)
				.HasColumnName("name")
				.HasMaxLength(255);
			});

			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasKey(p => p.Id)
				.HasName("product_pk");

				// entity.ToTable("Products");

				entity.Property(p => p.Name)
				.HasColumnName("name")
				.HasMaxLength(255);

				entity.HasOne(p => p.ProductGroup)
				.WithMany(p => p.Products).HasForeignKey(p => p.ProductGroupId);
			});

			modelBuilder.Entity<Storage>(entity =>
			{
				entity.HasKey(s => s.Id)
				.HasName("storage_pk");

				// entity.ToTable("Storage");

				entity.HasOne(s => s.Product)
				.WithMany(p => p.Storages).HasForeignKey(p => p.ProductId);
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}

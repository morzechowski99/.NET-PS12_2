using Microsoft.EntityFrameworkCore;
using PS12_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS12_2.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options ) :base(options)
        {

        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");
            modelBuilder.Entity<ProductCategory>().HasKey(c => new { c.ProductID, c.CategoryID });
            modelBuilder.Entity<ProductCategory>().HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(bc => bc.ProductID).OnDelete(DeleteBehavior.Cascade); 
            modelBuilder.Entity<ProductCategory>().HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(bc => bc.CategoryID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

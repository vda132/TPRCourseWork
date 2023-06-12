using Microsoft.EntityFrameworkCore;
using ProductService.DAL.Models;

namespace ProductService.DAL.ProductContext;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderProduct> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderProduct>().HasKey(p => new { p.OrderId, p.ProductId });
        base.OnModelCreating(modelBuilder);
    }
}

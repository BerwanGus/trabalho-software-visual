using APISale.Models;
using APIStock.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DBContext : DbContext
{

    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=Back2youDB.db;Cache=Shared");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>()
            .HasKey(b => b.Id);
        
        modelBuilder.Entity<ProductType>()
            .HasKey(pt => pt.Id);
        // Outras configurações do modelo...
    }

}
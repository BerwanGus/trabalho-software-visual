using APISale.Models;
using APIStock.Models;
using API.Models;
using Microsoft.EntityFrameworkCore; 

namespace API.Data;

public class DBContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<ProductSale> ProductsSales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=Back2youDB.db;Cache=Shared");
    }
}
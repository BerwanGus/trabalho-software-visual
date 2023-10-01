using APIStock.Models;
using Microsoft.EntityFrameworkCore; // Necessário instalar

namespace APIStock.Data;

public class StockDbContext : DbContext
{
    public DbSet<Product>? Product { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=inventario.db;Cache=Shared");
    }
}
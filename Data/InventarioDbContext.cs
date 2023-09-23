using APIInventario.Models;
using Microsoft.EntityFrameworCore;
using Microsoft.EntityFrameworkCore; // Necessário instalar

namespace APIInventario.Data;

public class InventarioDbContext : DbContext
{
    public DbSet<Camiseta>? Camisetas {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=inventario.db;Cache=Shared");
    }
}
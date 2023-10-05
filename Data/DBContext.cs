using APISale.Models;
using APIStock.Models;
using Microsoft.EntityFrameworkCore; 

namespace API.Data;

public class DBContext : DbContext
{
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
        modelBuilder.Entity<Event>()
            .HasMany(e => e.Sales)
            .WithOne(e => e.Event)
            .HasForeignKey(e => e.Event_Id)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Client>()
            .HasMany(e => e.Purchases)
            .WithOne(e => e.Client)
            .HasForeignKey(e => e.Client_Id)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Seller>()
            .HasMany(e => e.Sales)
            .WithOne(e => e.Seller)
            .HasForeignKey(e => e.Seller_Id)
            .HasPrincipalKey(e => e.Id);
    }
}
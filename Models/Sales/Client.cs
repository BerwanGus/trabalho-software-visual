using System.ComponentModel.DataAnnotations;

namespace APISale.Models;

public class Client
{
    [Key]
    public string? Id { get; set; }
    
    public required string Name { get; set; }
    public required string Cpf { get; set; }

    // ---- DEFAULT VALUE -> Generate DateTimeNow
    public DateTime Register_Date { get; set; }
    
    // ---- DEFAULT VALUE -> 0
    public required float Purchases_Quantity { get; set; }

    public ICollection<Sale>? Purchases { get; set; }

    public Client(string id, string name, string cpf, DateTime register, float qtty, ICollection<Sale> sales) {
        
    }
}

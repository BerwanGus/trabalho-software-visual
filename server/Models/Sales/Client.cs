using System.ComponentModel.DataAnnotations;

namespace APISale.Models;

public class Client
{
    [Key]
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Cpf { get; set; }
    
    // ---- DEFAULT VALUE -> Generate DateTimeNow
    public required DateTime Register_Date { get; set; }
    
    // ---- DEFAULT VALUE -> 0
    public required int Purchases_Quantity { get; set; }

    public virtual ICollection<Sale>? Purchases { get; set; }
}

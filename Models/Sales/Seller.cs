using System.ComponentModel.DataAnnotations;

namespace APISale.Models;

public class Seller
{
    [Key]
    public string? Id { get; set; }
    
    public required string Name { get; set; }
    public required string Cpf { get; set; }
    
    // ---- DEFAULT VALUE -> 0
    public required float Sales_Quantity { get; set; }

    public ICollection<Sale>? Sales { get; set; }
}

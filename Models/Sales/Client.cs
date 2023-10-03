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
    public required float Purchases_Quantity { get; set; }

    public ICollection<Sale>? Purchases { get; set; }

    // public Client(string id, string name, string cpf) {
    //     Id = id;
    //     Name = name;
    //     Cpf = cpf;
    //     Register_Date = DateTime.Now;
    //     Purchases_Quantity = 0;
    //     Purchases = new List<Sale>();
    // }
}

using System.ComponentModel.DataAnnotations;

namespace APISale.Models;

public class Event
{
    [Key]
    public string? Id { get; set; }
    
    public required string Name { get; set; }

    // ---- DEFAULT VALUE -> Generate Time Now
    public DateTime Event_Date { get; set; }
    
    // ---- DEFAULT VALUE -> 0
    public required float Sales_Quantity { get; set; }

    public ICollection<Sale>? Sales { get; set; }
}

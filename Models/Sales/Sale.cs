using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APISale.Models;

public class Sale
{
    [Key]
    public string? Id { get; set; }

    // ---- DEFAULT VALUE -> Generate DateTimeNow
    public DateTime Sale_Date { get; set; }
    public required float Value { get; set; }

    public required string Client_Id { get; set; }
    public required string Seller_Id { get; set; }
    public required string Event_Id { get; set; }

    [ForeignKey("Client_Id")]
    public Client? Client {get; set; }

    [ForeignKey("Seller_Id")]
    public Seller? Seller {get; set; }

    [ForeignKey("Event_Id")]
    public Event? Event {get; set; }
}

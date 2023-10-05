using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Models;

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
    public virtual Client Client {get; set; }

    [ForeignKey("Seller_Id")]
    public virtual Seller Seller {get; set; }

    [ForeignKey("Event_Id")]
    public virtual Event Event {get; set; }

    public virtual ICollection<ProductSale>? ProductSales {get; set; }
}


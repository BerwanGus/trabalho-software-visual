using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APISale.Models;
using APIStock.Models;

namespace API.Models;

public class ProductSale
{
    [Key]
    public string? Id { get; set; }

    public required int ProductQuantity { get; set; }

    public required string ProductId { get; set; }
    public required string SaleId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("SaleId")]
    public virtual Sale Sale { get; set; }
}


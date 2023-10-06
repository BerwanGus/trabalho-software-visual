using System.ComponentModel.DataAnnotations;
using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIStock.Models;

public class Product
{
    [Key]
    public string Id { get; set; }
    public string? Size { get; set; }
    public string? Gender { get; set; }
    public string? Style { get; set; }
    public string? Condition { get; set; }
    public float Cost { get; set; }
    public float Price { get; set; }
    public string Color { get; set; }
    public required string Brand_Id { get; set; }
    public required string Type_Id { get; set; }

    [ForeignKey("Brand_Id")]
    public virtual Brand Brand { get; set; }

    [ForeignKey("Type_Id")]
    public virtual ProductType ProductType { get; set; }

    public virtual ICollection<ProductSale>? ProductSales { get; set; }
}

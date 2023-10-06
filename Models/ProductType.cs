using System.ComponentModel.DataAnnotations;

namespace APIStock.Models;

public class ProductType
{
  [Key]
  public required string Id { get; set; }
  
  public required string TypeName { get; set; }
  public required string Style { get; set; }

  public virtual ICollection<Product>? Products { get; set; }
}

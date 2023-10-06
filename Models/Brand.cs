using System.ComponentModel.DataAnnotations;

namespace APIStock.Models;

public class Brand
{
  [Key]
  public required string Id { get; set; }
  
  public string? Name { get; set; }
  
  public virtual ICollection<Product>? Products { get; set; }
}

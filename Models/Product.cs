using System.ComponentModel.DataAnnotations;

namespace APIStock.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string? Size { get; set; }
    public string? Gender { get; set; }
    public string? Style { get; set; }
    public string? Condition { get; set; }
    public float Cost { get; set; }
    public float Price { get; set; }
    public string? Brand { get; set; }
    public string? Color { get; set; }
}

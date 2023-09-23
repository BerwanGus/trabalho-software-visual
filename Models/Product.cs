using System.ComponentModel.DataAnnotations;

namespace APIInventario.Models;

public class Product
{
    [Key]
    public int Id {get; set;}
    public string? Size {get; set;}
    public string? Gender {get; set;}
    public string? Style {get; set;}
}

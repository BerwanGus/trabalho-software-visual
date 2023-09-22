using System.ComponentModel.DataAnnotations;

namespace APIInventario.Models;

public class Camiseta
{
    [Key]
    public int Codigo {get; set;}
    public string? Tamanho {get; set;}
    public string? Genero {get; set;}
    public string? Estilo {get; set;}
}
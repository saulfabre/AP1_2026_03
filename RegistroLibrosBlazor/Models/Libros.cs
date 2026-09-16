using System.ComponentModel.DataAnnotations;

namespace RegistroLibrosBlazor.Models;

public class Libros
{
    [Key]  

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public int LibroId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Titulo { get; set; } = null!;

    [Required(ErrorMessage = "Este campo es obligatorio")]
    public string Autor { get; set; } = null!;
    
    [Required(ErrorMessage = "Este campo es obligatorio")]
    public DateOnly AnoPublicacion { get; set; }
}
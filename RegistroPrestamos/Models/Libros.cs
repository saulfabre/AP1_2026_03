using System.ComponentModel.DataAnnotations;

namespace RegistroPrestamos.Models;

public class Libros
{
    [Key]
    public int LibroId { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    public string Titulo { get; set; } = "";

    [Required(ErrorMessage = "El autor es obligatorio.")]
    public string Autor { get; set; } = "";

    [Required(ErrorMessage = "El año de publicación es obligatorio.")]
    public DateOnly AnoPublicacion { get; set; }
}

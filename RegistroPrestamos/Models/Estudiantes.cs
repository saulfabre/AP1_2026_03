using System.ComponentModel.DataAnnotations;

namespace RegistroPrestamos.Models;

public class Estudiantes
{
    [Key]
    public int EstudianteId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombres { get; set; } = "";

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    public string Direccion { get; set; } = "";

    [Required(ErrorMessage = "El Email es obligatorio.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    public DateOnly FechaNacimiento { get; set; }
}

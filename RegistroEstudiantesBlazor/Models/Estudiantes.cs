using System.ComponentModel.DataAnnotations;

namespace RegistroEstudiantesBlazor.Models;

public class Estudiantes
{
    [Key]
    public int EstudianteId { get; set; }

    [Required(ErrorMessage="El nombre es obligatorio.")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage="La dirección es obligatoria.")]
    public string Direccion { get; set; } = string.Empty; 

    [Required(ErrorMessage="El correo electrónico es obligatorio.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage="La fecha de nacimiento es obligatoria.")]
    public DateOnly FechaNacimiento { get; set; } 
}
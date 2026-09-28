using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroPrestamos.Models;

public class Prestamos
{
    [Key]
    public int PrestamoId { get; set; }

    [Required(ErrorMessage = "La fecha de prestamo es obligatoria.")]
    public DateOnly FechaPrestamo { get; set; }

    public DateOnly FechaDevolucion { get; set; }

    [ForeignKey("LibroId")]
    public Libros LibroId { get; set; }  = null!;

    [ForeignKey("EstudianteId")]
    public Estudiantes EstudianteId { get; set; } = null!;
}
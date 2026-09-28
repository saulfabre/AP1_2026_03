using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroPrestamos.Models;

public class Prestamos
{
    [Key]
    public int PrestamoId { get; set; }

    public int LibroId { get; set ; }

    public int EstudianteId { get; set; }

    [Required(ErrorMessage = "La fecha de prestamo es obligatoria.")]
    public DateOnly FechaPrestamo { get; set; }

    public DateOnly FechaDevolucion { get; set; }

    [ForeignKey("LibroId")]
    public Libros Libro { get; set; }  = null!;

    [ForeignKey("EstudianteId")]
    public Estudiantes Estudiante { get; set; } = null!;
}
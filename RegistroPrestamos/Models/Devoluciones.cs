using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroPrestamos.Models;

public class Devoluciones
{
    [Key]
    public int DevolucionId { get; set; }

    [Required(ErrorMessage = "La fecha de devolución es obligatoria.")]
    public DateOnly FechaDevolucion { get; set; }

    [ForeignKey("PrestamoId")]
    public int PrestamoId { get; set; }

    public Prestamos Prestamo { get; set; } = null!;
}

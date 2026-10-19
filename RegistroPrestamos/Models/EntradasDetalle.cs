using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroPrestamos.Models;

public class EntradasDetalle
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("EntradaId")]
    public int EntradaId { get; set; }

    public Entradas Entrada { get; set; } = null!;

    [ForeignKey("LibroId")]
    public int LibroId { get; set; }

    public Libros Libro { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
    public int Cantidad { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace RegistroPrestamos.Models;

public class Entradas
{
    [Key]
    public int EntradaId { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateOnly Fecha { get; set; }

    [Required(ErrorMessage = "El concepto es obligatorio.")]
    public string Concepto { get; set; } = "";

    public int Total { get; set; }

    public List<EntradasDetalle> Detalles { get; set; } = new();
}

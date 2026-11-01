using System;
using System.Collections.Generic;

namespace RegistroPrestamos.Data;

public partial class Libro
{
    public int LibroId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public DateOnly AnoPublicacion { get; set; }

    public bool Prestado { get; set; }

    public int Cantidad { get; set; }

    public virtual ICollection<EntradasDetalle> EntradasDetalles { get; set; } = new List<EntradasDetalle>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}

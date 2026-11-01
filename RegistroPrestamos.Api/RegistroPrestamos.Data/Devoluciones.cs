using System;
using System.Collections.Generic;

namespace RegistroPrestamos.Data;

public partial class Devoluciones
{
    public int DevolucionId { get; set; }

    public DateOnly FechaDevolucion { get; set; }

    public int PrestamoId { get; set; }

    public virtual Prestamo Prestamo { get; set; } = null!;
}

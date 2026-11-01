using System;
using System.Collections.Generic;

namespace RegistroPrestamos.Data;

public partial class Prestamo
{
    public int PrestamoId { get; set; }

    public int LibroId { get; set; }

    public int EstudianteId { get; set; }

    public DateOnly FechaPrestamo { get; set; }

    public DateOnly FechaDevolucion { get; set; }

    public bool Devuelto { get; set; }

    public virtual Devolucione? Devolucione { get; set; }

    public virtual Estudiante Estudiante { get; set; } = null!;

    public virtual Libro Libro { get; set; } = null!;
}

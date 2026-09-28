using Microsoft.EntityFrameworkCore;
using RegistroPrestamos.Models;

namespace RegistroPrestamos.Context;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
        
    }

    public DbSet<Libros> Libros { get; set; }

    public DbSet<Estudiantes> Estudiantes { get; set; }

    public DbSet<Prestamos> Prestamos { get; set; }
}

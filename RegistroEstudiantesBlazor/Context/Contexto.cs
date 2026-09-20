using Microsoft.EntityFrameworkCore;
using RegistroEstudiantesBlazor.Models;

namespace RegistroEstudiantesBlazor.Context;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
        
    }   
    public DbSet<Estudiantes> Estudiantes { get; set; }
}
using System.Linq.Expressions;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using RegistroPrestamos.Context;
using RegistroPrestamos.Models;

namespace RegistroPrestamos.Services;

public class PrestamosServices(IDbContextFactory<Contexto> contextFactory
    ) : IService<Prestamos, int>
{
    public async Task<Prestamos?> Buscar(int prestamoId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Prestamos.Include(p=> p.Estudiante).Include(p => p.Libro).FirstOrDefaultAsync(p => p.PrestamoId == prestamoId);
    }

    public async Task<List<Prestamos>> GetList(Expression<Func<Prestamos, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Prestamos.Include(p => p.Estudiante).Include(p => p.Libro).Where(criterio).AsNoTracking().ToListAsync();
    }

    public async Task<bool> Existe(int prestamoId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Prestamos.AnyAsync(p => p.PrestamoId == prestamoId);
    }

    public async Task<bool> Insertar(Prestamos prestamo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.Prestamos.Add(prestamo);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Prestamos prestamo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var Existe = await contexto.Prestamos.FirstOrDefaultAsync(p => p.PrestamoId == prestamo.PrestamoId);

        if (Existe != null)
        {
            Existe.EstudianteId = prestamo.EstudianteId;
            Existe.LibroId = prestamo.LibroId;
            Existe.FechaPrestamo = prestamo.FechaPrestamo;
            Existe.FechaDevolucion = prestamo.FechaDevolucion;
            Existe.Devuelto = prestamo.Devuelto;
        }
        
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int prestamoId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Prestamos.Where(p => p.PrestamoId == prestamoId).ExecuteDeleteAsync() > 0;
    }

    public async Task<bool> Guardar(Prestamos prestamo)
    {
        if (!await Existe(prestamo.PrestamoId))
        {
            return await Insertar(prestamo);
        }
        else
        {
            return await Modificar(prestamo);
        }
    }
}

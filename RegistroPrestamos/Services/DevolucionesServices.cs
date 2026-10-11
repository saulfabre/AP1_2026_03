using System.Linq.Expressions;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using RegistroPrestamos.Context;
using RegistroPrestamos.Models;

namespace RegistroPrestamos.Services;

public class DevolucionesServices(IDbContextFactory<Contexto> contextFactory
    ) : IService<Devoluciones, int>
{
    public async Task<Devoluciones?> Buscar(int devolucionId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Devoluciones.Include(d => d.Prestamo).FirstOrDefaultAsync(d => d.DevolucionId == devolucionId);
    }

    public async Task<List<Devoluciones>> GetList(Expression<Func<Devoluciones, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Devoluciones.Include(d => d.Prestamo).Where(criterio).AsNoTracking().ToListAsync();
    }

    public async Task<bool> Existe(int devolucionId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Devoluciones.AnyAsync(d => d.DevolucionId == devolucionId);
    }

    public async Task<bool> Insertar(Devoluciones devolucion)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.Devoluciones.Add(devolucion);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Devoluciones devolucion)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var Existe = await contexto.Devoluciones.FirstOrDefaultAsync(d => d.DevolucionId == devolucion.DevolucionId);

        if (Existe != null)
        {
            Existe.PrestamoId = devolucion.PrestamoId;
            Existe.FechaDevolucion = devolucion.FechaDevolucion;
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int devolucionId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Devoluciones.Where(d => d.DevolucionId == devolucionId).ExecuteDeleteAsync() > 0;
    }

    public async Task<bool> Guardar(Devoluciones devolucion)
    {
        if (!await Existe(devolucion.DevolucionId))
        {
            return await Insertar(devolucion);
        }
        else
        {
            return await Modificar(devolucion);
        }
    }
}

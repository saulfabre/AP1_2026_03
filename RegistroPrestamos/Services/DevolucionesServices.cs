using System.Linq.Expressions;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using RegistroPrestamos.Context;
using RegistroPrestamos.Models;

namespace RegistroPrestamos.Services;

public class DevolucionesServices(IDbContextFactory<Contexto> contextFactory
    ) : IServiceResult<Devoluciones, int>
{
    public async Task<Result> Guardar(Devoluciones devolucion)
    {
        if (!await Existe(devolucion.DevolucionId))
        {
            var inserto = await Insertar(devolucion);
            return inserto
                ? Result.Success()
                : Result.Failure(new Error("DEVOLUCION_INSERT", "No se pudo guardar la devolucion."));
        }
        else
        {
            var modifico = await Modificar(devolucion);
            return modifico
                ? Result.Success()
                : Result.Failure(new Error("DEVOLUCION_UPDATE", "No se pudo actualizar la devolucion."));
        }
    }

    private async Task<bool> Existe(int devolucionId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Devoluciones
            .AnyAsync(d => d.DevolucionId == devolucionId);
    }

    private async Task<bool> Insertar(Devoluciones devolucion)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Devoluciones.Add(devolucion);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Devoluciones devolucion)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Update(devolucion);
        return await contexto
            .SaveChangesAsync() > 0;
    }

    public async Task<Result<Devoluciones?>> Buscar(int devolucionId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Devoluciones
            .Include(d => d.Prestamo)
            .FirstOrDefaultAsync(d => d.DevolucionId == devolucionId);
    }

    public async Task<Result<List<Devoluciones>>> GetList(Expression<Func<Devoluciones, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Devoluciones
            .Include(d => d.Prestamo)
            .ThenInclude(p => p.Estudiante)
            .Include(d => d.Prestamo)
            .ThenInclude(p => p.Libro)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Result> Eliminar(int devolucionId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var eliminado = await contexto.Devoluciones
            .Where(d => d.DevolucionId == devolucionId)
            .ExecuteDeleteAsync() > 0;
        return eliminado
            ? Result.Success()
            : Result.Failure(new Error("DEVOLUCION_DELETE", "No se pudo eliminar la devolucion."));
    }
}

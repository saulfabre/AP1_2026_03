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

        return await contexto.Prestamos.FirstOrDefaultAsync(p => p.PrestamoId == prestamoId);
    }

    public async Task<List<Prestamos>> GetList(Expression<Func<Prestamos, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Prestamos.Where(criterio).AsNoTracking().ToListAsync();
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

        contexto.Update(prestamo);

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

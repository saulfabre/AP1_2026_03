using System.Linq.Expressions;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using RegistroPrestamos.Data;

namespace RegistroPrestamos.Services;

public class PrestamosServices(IDbContextFactory<RegistroPrestamosDbContext> contextFactory
    ) : IServiceResult<Prestamo, int>
{
    public async Task<Result<Prestamo?>> Buscar(int prestamoId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Prestamos
            .Include(p => p.Estudiante)
            .Include(p => p.Libro)
            .FirstOrDefaultAsync(p => p.PrestamoId == prestamoId);
    }

    public async Task<Result<List<Prestamo>>> GetList(Expression<Func<Prestamo, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Prestamos
            .Include(p => p.Estudiante)
            .Include(p => p.Libro)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Existe(int prestamoId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Prestamos
            .AnyAsync(p => p.PrestamoId == prestamoId);
    }

    public async Task<bool> Insertar(Prestamo prestamo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Prestamos.Add(prestamo);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Prestamo prestamo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Update(prestamo);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Result> Eliminar (int prestamoId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var eliminado = await contexto.Prestamos
            .Where(p => p.PrestamoId == prestamoId)
            .ExecuteDeleteAsync() > 0;
        return Result.Success(eliminado);
    }

    public async Task<Result> Guardar(Prestamo prestamo)
    {
       if (!await Existe(prestamo.PrestamoId))
        {
            return Result.Success(await Insertar(prestamo));
        }
        else
        {
            return Result.Success(await Modificar(prestamo));
        }
    }
}

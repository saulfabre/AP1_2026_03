using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroLibrosBlazor.Models;

namespace RegistroLibrosBlazor.Services;

public class LibrosService(
    IDbContextFactory<Contexto> contextFactory
) : Aplicada1.Core.IService<Libros, int>
{
    public async Task<Libros?> Buscar(int libroId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Libros
            .FirstOrDefaultAsync(l => l.LibroId == libroId);
    }

    public async Task<bool> Insertar(Libros libro)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Libros.Add(libro);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int libroId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Libros
            .Where(l => l.LibroId == libroId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<bool> Modificar(Libros libro)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Update(libro);
        return await contexto
            .SaveChangesAsync() > 0;       
    }

    public async Task<List<Libros>> GetList(Expression<Func<Libros, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Libros
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Existe(string titulo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Libros
            .AnyAsync(l => l.Titulo == titulo);
    }

    public async Task<bool> Guardar(Libros libro)
    {
        if (!await Existe(libro.Titulo))
        {
            return await Insertar(libro);
        }
        else
        {
            return await Modificar(libro);
        }
    }
}
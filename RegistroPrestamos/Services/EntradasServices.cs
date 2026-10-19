using System.Linq.Expressions;
using Aplicada1.Core;
using Microsoft.EntityFrameworkCore;
using RegistroPrestamos.Context;
using RegistroPrestamos.Models;

namespace RegistroPrestamos.Services;

public class EntradasServices(IDbContextFactory<Contexto> contextFactory
    ) : IService<Entradas, int>
{
    public async Task<bool> Existe(int entradaId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Entradas.AnyAsync(e => e.EntradaId == entradaId);
    }

    public async Task<bool> Guardar(Entradas entrada)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        if (entrada.Detalles.Count == 0 || entrada.Detalles.Any(d => d.LibroId <= 0 || d.Cantidad <= 0))
        {
            return false;
        }

        var libroIds = entrada.Detalles.Select(d => d.LibroId).Distinct().ToList();
        var libros = await contexto.Libros.Where(l => libroIds.Contains(l.LibroId)).ToListAsync();

        if (libros.Count != libroIds.Count)
        {
            return false;
        }

        foreach (var detalle in entrada.Detalles)
        {
            var libro = libros.First(l => l.LibroId == detalle.LibroId);
            if (libro.Cantidad + detalle.Cantidad > int.MaxValue)
            {
                return false;
            }
            libro.Cantidad += detalle.Cantidad;
        }

        entrada.Total = entrada.Detalles.Sum(d => d.Cantidad);
        entrada.Detalles = entrada.Detalles.Select(d => new EntradasDetalle
        {
            LibroId = d.LibroId,
            Cantidad = d.Cantidad
        }).ToList();

        contexto.Entradas.Add(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Entradas entrada)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        if (entrada.Detalles.Count == 0 || entrada.Detalles.Any(d => d.LibroId <= 0 || d.Cantidad <= 0))
        {
            return false;
        }

        var entradaExistente = await contexto.Entradas.Include(e => e.Detalles)
            .FirstOrDefaultAsync(e => e.EntradaId == entrada.EntradaId);

        if (entradaExistente == null)
        {
            return false;
        }

        var libroIds = entradaExistente.Detalles.Select(d => d.LibroId)
            .Union(entrada.Detalles.Select(d => d.LibroId)).Distinct().ToList();
        var libros = await contexto.Libros.Where(l => libroIds.Contains(l.LibroId)).ToListAsync();

        if (libros.Count != libroIds.Count)
        {
            return false;
        }

        foreach (var libro in libros)
        {
            var cantidadAnterior = entradaExistente.Detalles
                .Where(d => d.LibroId == libro.LibroId).Sum(d => d.Cantidad);
            var cantidadNueva = entrada.Detalles
                .Where(d => d.LibroId == libro.LibroId).Sum(d => d.Cantidad);
            var cantidadActualizada = libro.Cantidad + cantidadNueva - cantidadAnterior;

            if (cantidadActualizada < 0 || cantidadActualizada > int.MaxValue)
            {
                return false;
            }

            libro.Cantidad = cantidadActualizada;
        }

        contexto.EntradasDetalle.RemoveRange(entradaExistente.Detalles);
        entradaExistente.Fecha = entrada.Fecha;
        entradaExistente.Concepto = entrada.Concepto;
        entradaExistente.Total = entrada.Detalles.Sum(d => d.Cantidad);
        entradaExistente.Detalles = entrada.Detalles.Select(d => new EntradasDetalle
        {
            EntradaId = entrada.EntradaId,
            LibroId = d.LibroId,
            Cantidad = d.Cantidad
        }).ToList();
        contexto.EntradasDetalle.AddRange(entradaExistente.Detalles);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Entradas?> Buscar(int entradaId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Entradas.Include(e => e.Detalles).ThenInclude(d => d.Libro)
            .FirstOrDefaultAsync(e => e.EntradaId == entradaId);
    }

    public async Task<bool> Eliminar(int entradaId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var entrada = await contexto.Entradas.Include(e => e.Detalles)
            .FirstOrDefaultAsync(e => e.EntradaId == entradaId);

        if (entrada == null)
        {
            return false;
        }

        var libroIds = entrada.Detalles.Select(d => d.LibroId).Distinct().ToList();
        var libros = await contexto.Libros.Where(l => libroIds.Contains(l.LibroId)).ToListAsync();

        if (libros.Count != libroIds.Count)
        {
            return false;
        }

        foreach (var detalle in entrada.Detalles)
        {
            var libro = libros.First(l => l.LibroId == detalle.LibroId);
            if (libro.Cantidad < detalle.Cantidad)
            {
                return false;
            }
            libro.Cantidad -= detalle.Cantidad;
        }

        contexto.Entradas.Remove(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Entradas>> GetList(Expression<Func<Entradas, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Entradas.Include(e => e.Detalles)
            .Where(criterio).AsNoTracking().ToListAsync();
    }
}

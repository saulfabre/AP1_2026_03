using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroEstudiantesBlazor.Context;
using RegistroEstudiantesBlazor.Models;

namespace RegistroEstudiantesBlazor.Services;

public class EstudiantesServices(IDbContextFactory<Contexto> contextFactory
) : Aplicada1.Core.IService<Estudiantes, int>

{

    public async Task<Estudiantes?> Buscar(int estudianteId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Estudiantes
        .FirstOrDefaultAsync(e => e.EstudianteId == estudianteId);
    }

    public async Task<List<Estudiantes>> GetList(Expression<Func<Estudiantes, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Estudiantes
        .Where(criterio).ToListAsync();
    }

    public async Task<bool> Insertar(Estudiantes estudiante)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Add(estudiante);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Estudiantes estudiante)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Update(estudiante);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int estudianteId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Estudiantes
        .Where(e => e.EstudianteId == estudianteId).ExecuteDeleteAsync() > 0;
    }

    public async Task<bool> Existe(string nombresEstudiante)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Estudiantes
        .AnyAsync(e => e.Nombres == nombresEstudiante);
    }

    public async Task<bool> Guardar(Estudiantes estudiante)
    {
        if (!await Existe(estudiante.Nombres))
        {
            return await Insertar(estudiante);
        }
        else
        {
           return await Modificar(estudiante);
        }
    }
}
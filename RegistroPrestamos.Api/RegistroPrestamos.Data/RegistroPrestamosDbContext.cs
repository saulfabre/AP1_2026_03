using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RegistroPrestamos.Data;

public partial class RegistroPrestamosDbContext : DbContext
{
    public RegistroPrestamosDbContext(DbContextOptions<RegistroPrestamosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Devoluciones> Devoluciones { get; set; }

    public virtual DbSet<Entrada> Entradas { get; set; }

    public virtual DbSet<EntradasDetalle> EntradasDetalles { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Devoluciones>(entity =>
        {
            entity.HasKey(e => e.DevolucionId);

            entity.HasIndex(e => e.PrestamoId, "IX_Devoluciones_PrestamoId").IsUnique();

            entity.HasOne(d => d.Prestamo).WithOne(p => p.Devoluciones).HasForeignKey<Devoluciones>(d => d.PrestamoId);
        });

        modelBuilder.Entity<EntradasDetalle>(entity =>
        {
            entity.ToTable("EntradasDetalle");

            entity.HasIndex(e => e.EntradaId, "IX_EntradasDetalle_EntradaId");

            entity.HasIndex(e => e.LibroId, "IX_EntradasDetalle_LibroId");

            entity.HasOne(d => d.Entrada).WithMany(p => p.EntradasDetalles).HasForeignKey(d => d.EntradaId);

            entity.HasOne(d => d.Libro).WithMany(p => p.EntradasDetalles).HasForeignKey(d => d.LibroId);
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasIndex(e => e.EstudianteId, "IX_Prestamos_EstudianteId");

            entity.HasIndex(e => e.LibroId, "IX_Prestamos_LibroId");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Prestamos).HasForeignKey(d => d.EstudianteId);

            entity.HasOne(d => d.Libro).WithMany(p => p.Prestamos).HasForeignKey(d => d.LibroId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

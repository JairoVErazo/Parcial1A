using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Parcial1A.Models;

public partial class AutoresdbContext : DbContext
{
    public AutoresdbContext()
    {
    }

    public AutoresdbContext(DbContextOptions<AutoresdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AutorLibro> AutorLibros { get; set; }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Lily\\MSSQLSERVER01; Database=autoresdb; Integrated Security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AutorLibro>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AutorLibro");

            entity.HasOne(d => d.Autor).WithMany()
                .HasForeignKey(d => d.AutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AutorLibro_Autores");

            entity.HasOne(d => d.Libro).WithMany()
                .HasForeignKey(d => d.LibroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AutorLibro_Libros");
        });

        modelBuilder.Entity<Autore>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(e => e.Contenido)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaPublicacion).HasColumnType("datetime");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Autor).WithMany(p => p.Posts)
                .HasForeignKey(d => d.AutorId)
                .HasConstraintName("FK_Posts_Autores");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

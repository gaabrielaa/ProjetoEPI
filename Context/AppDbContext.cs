using System;
using System.Collections.Generic;
using APIprojeto.Models;
using Microsoft.EntityFrameworkCore;

namespace APIprojeto.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cadastro> Cadastros { get; set; }

    public virtual DbSet<Colaborador> Colaboradors { get; set; }

    public virtual DbSet<Entrega> Entregas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=pro_EPI;UserId=postgres;Password=senai901;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cadastro>(entity =>
        {
            entity.HasKey(e => e.CodEpi).HasName("cadastro_pkey");

            entity.ToTable("cadastro");

            entity.HasIndex(e => e.CodEpi, "idx_cod_epi");

            entity.HasIndex(e => e.UsuEpi, "idx_uso_epi");

            entity.Property(e => e.CodEpi).HasColumnName("cod_epi");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.UsuEpi)
                .HasMaxLength(500)
                .HasColumnName("usu_epi");
        });

        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.HasKey(e => e.CodCol).HasName("colaborador_pkey");

            entity.ToTable("colaborador");

            entity.HasIndex(e => e.Cpf, "colaborador_cpf_key").IsUnique();

            entity.HasIndex(e => e.CodCol, "idx_cod_col");

            entity.HasIndex(e => e.Cpf, "idx_cod_cpf");

            entity.HasIndex(e => e.Ctps, "idx_cod_ctps");

            entity.HasIndex(e => e.Ctps, "uk_ctps").IsUnique();

            entity.Property(e => e.CodCol).HasColumnName("cod_col");
            entity.Property(e => e.Cpf).HasColumnName("cpf");
            entity.Property(e => e.Ctps)
                .HasMaxLength(20)
                .HasColumnName("ctps");
            entity.Property(e => e.DtAdm).HasColumnName("dt_adm");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Tel).HasColumnName("tel");
        });

        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.CodEntrega).HasName("entrega_pkey");

            entity.ToTable("entrega");

            entity.HasIndex(e => e.CodEntrega, "idx_cod_entrega");

            entity.HasIndex(e => new { e.CodCol, e.CodEpi }, "idx_cod_epi_col");

            entity.Property(e => e.CodEntrega).HasColumnName("cod_entrega");
            entity.Property(e => e.CodCol).HasColumnName("cod_col");
            entity.Property(e => e.CodEpi).HasColumnName("cod_epi");
            entity.Property(e => e.DtEntrega).HasColumnName("dt_entrega");
            entity.Property(e => e.DtVal).HasColumnName("dt_val");

            entity.HasOne(d => d.CodColNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.CodCol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrega_cod_col_fkey");

            entity.HasOne(d => d.CodEpiNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.CodEpi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entrega_cod_epi_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

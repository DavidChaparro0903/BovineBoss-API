using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BovineBoss_API.Models.DB;

public partial class BovineBossContext : DbContext
{
    public BovineBossContext()
    {
    }

    public BovineBossContext(DbContextOptions<BovineBossContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdministradorFinca> AdministradorFincas { get; set; }

    public virtual DbSet<Adquisicione> Adquisiciones { get; set; }

    public virtual DbSet<Alimento> Alimentos { get; set; }

    public virtual DbSet<Finca> Fincas { get; set; }

    public virtual DbSet<FincaAlimento> FincaAlimentos { get; set; }

    public virtual DbSet<FincaGasto> FincaGastos { get; set; }

    public virtual DbSet<Gasto> Gastos { get; set; }

    public virtual DbSet<HistorialAlimentacion> HistorialAlimentacions { get; set; }

    public virtual DbSet<HistorialPeso> HistorialPesos { get; set; }

    public virtual DbSet<Inconveniente> Inconvenientes { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Raza> Razas { get; set; }

    public virtual DbSet<ResInconveniente> ResInconvenientes { get; set; }

    public virtual DbSet<ResRaza> ResRazas { get; set; }

    public virtual DbSet<Rese> Reses { get; set; }

    public virtual DbSet<TrabajadorFinca> TrabajadorFincas { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost; Database=BovineBoss; User IdFinca = sa; Password=123 ;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<AdministradorFinca>(entity =>
        {
            entity.HasKey(e => new { e.IdFinca, e.IdAdministrador });

            entity.ToTable("Administrador_Finca");

            entity.Property(e => e.IdFinca).HasColumnName("id_finca");
            entity.Property(e => e.IdAdministrador).HasColumnName("id_administrador");
            entity.Property(e => e.EstadoAdministrador).HasColumnName("estado_administrador");

            entity.HasOne(d => d.IdAdministradorNavigation).WithMany(p => p.AdministradorFincas)
                .HasForeignKey(d => d.IdAdministrador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Administrador_Finca_Personas");

            entity.HasOne(d => d.IdFincaNavigation).WithMany(p => p.AdministradorFincas)
                .HasForeignKey(d => d.IdFinca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Administrador_Finca_Fincas");
        });

        modelBuilder.Entity<Adquisicione>(entity =>
        {
            entity.HasKey(e => new { e.FechaAdquisicion, e.IdRes, e.IdPropietario });

            entity.Property(e => e.FechaAdquisicion)
                .HasColumnType("date")
                .HasColumnName("fecha_adquisicion");
            entity.Property(e => e.IdRes).HasColumnName("id_res");
            entity.Property(e => e.IdPropietario).HasColumnName("id_propietario");
            entity.Property(e => e.ComisionesPagada).HasColumnName("comisiones_pagada");
            entity.Property(e => e.CostoCompraRes).HasColumnName("costo_compra_res");
            entity.Property(e => e.DescripcionAdquisicion)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("descripcion_adquisicion");
            entity.Property(e => e.PrecioFlete).HasColumnName("precio_flete");

            entity.HasOne(d => d.IdPropietarioNavigation).WithMany(p => p.Adquisiciones)
                .HasForeignKey(d => d.IdPropietario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Adquisiciones_Personas");

            entity.HasOne(d => d.IdResNavigation).WithMany(p => p.Adquisiciones)
                .HasForeignKey(d => d.IdRes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Adquisiciones_Reses");
        });

        modelBuilder.Entity<Alimento>(entity =>
        {
            entity.HasKey(e => e.IdAlimento);

            entity.Property(e => e.IdAlimento).HasColumnName("id_alimento");
            entity.Property(e => e.TipoAlimento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_alimento");
        });

        modelBuilder.Entity<Finca>(entity =>
        {
            entity.HasKey(e => e.IdFinca);

            entity.Property(e => e.IdFinca).HasColumnName("id_finca");
            entity.Property(e => e.DireccionFinca)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion_finca");
            entity.Property(e => e.ExtensionFinca).HasColumnName("extension_finca");
            entity.Property(e => e.NombreFinca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_finca");
        });

        modelBuilder.Entity<FincaAlimento>(entity =>
        {
            entity.HasKey(e => new { e.FechaCompra, e.IdFinca, e.IdAlimento });

            entity.ToTable("Finca_Alimento");

            entity.Property(e => e.FechaCompra)
                .HasColumnType("date")
                .HasColumnName("fecha_compra");
            entity.Property(e => e.IdFinca).HasColumnName("id_finca");
            entity.Property(e => e.IdAlimento).HasColumnName("id_alimento");
            entity.Property(e => e.CantidadComprada).HasColumnName("cantidad_comprada");
            entity.Property(e => e.NombreAlimento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_alimento");
            entity.Property(e => e.PrecioAlimento).HasColumnName("precio_alimento");

            entity.HasOne(d => d.IdAlimentoNavigation).WithMany(p => p.FincaAlimentos)
                .HasForeignKey(d => d.IdAlimento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finca_Alimento_Alimentos");

            entity.HasOne(d => d.IdFincaNavigation).WithMany(p => p.FincaAlimentos)
                .HasForeignKey(d => d.IdFinca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finca_Alimento_Fincas");
        });

        modelBuilder.Entity<FincaGasto>(entity =>
        {
            entity.HasKey(e => new { e.FechaGasto, e.IdFinca, e.IdGasto });

            entity.ToTable("Finca_Gastos");

            entity.Property(e => e.FechaGasto)
                .HasColumnType("date")
                .HasColumnName("fecha_gasto");
            entity.Property(e => e.IdFinca).HasColumnName("id_finca");
            entity.Property(e => e.IdGasto).HasColumnName("id_gasto");
            entity.Property(e => e.DescripcionGasto)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descripcion_gasto");
            entity.Property(e => e.ValorGasto).HasColumnName("valor_gasto");

            entity.HasOne(d => d.IdFincaNavigation).WithMany(p => p.FincaGastos)
                .HasForeignKey(d => d.IdFinca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finca_Gastos_Fincas");

            entity.HasOne(d => d.IdGastoNavigation).WithMany(p => p.FincaGastos)
                .HasForeignKey(d => d.IdGasto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Finca_Gastos_Gastos");
        });

        modelBuilder.Entity<Gasto>(entity =>
        {
            entity.HasKey(e => e.IdGasto);

            entity.Property(e => e.IdGasto)
                .ValueGeneratedNever()
                .HasColumnName("id_gasto");
            entity.Property(e => e.TipoGasto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_gasto");
        });

        modelBuilder.Entity<HistorialAlimentacion>(entity =>
        {
            entity.HasKey(e => new { e.FechaAlimentacion, e.IdRes, e.FechaCompra, e.IdAlimento, e.IdFinca });

            entity.ToTable("Historial_Alimentacion");

            entity.Property(e => e.FechaAlimentacion)
                .HasColumnType("date")
                .HasColumnName("fecha_alimentacion");
            entity.Property(e => e.IdRes).HasColumnName("id_res");
            entity.Property(e => e.FechaCompra)
                .HasColumnType("date")
                .HasColumnName("fecha_compra");
            entity.Property(e => e.IdAlimento).HasColumnName("id_alimento");
            entity.Property(e => e.IdFinca).HasColumnName("id_finca");
            entity.Property(e => e.CantidadAlimentacion).HasColumnName("cantidad_alimentacion");

            entity.HasOne(d => d.IdResNavigation).WithMany(p => p.HistorialAlimentacions)
                .HasForeignKey(d => d.IdRes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Alimentacion_Reses");

            entity.HasOne(d => d.FincaAlimento).WithMany(p => p.HistorialAlimentacions)
                .HasForeignKey(d => new { d.FechaCompra, d.IdFinca, d.IdAlimento })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Alimentacion_Finca_Alimento");
        });

        modelBuilder.Entity<HistorialPeso>(entity =>
        {
            entity.HasKey(e => new { e.FechaAlimentacion, e.IdRes });

            entity.ToTable("Historial_Peso");

            entity.Property(e => e.FechaAlimentacion)
                .HasColumnType("date")
                .HasColumnName("fecha_alimentacion");
            entity.Property(e => e.IdRes).HasColumnName("id_res");
            entity.Property(e => e.PesoRes).HasColumnName("peso_res");

            entity.HasOne(d => d.IdResNavigation).WithMany(p => p.HistorialPesos)
                .HasForeignKey(d => d.IdRes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historial_Peso_Reses");
        });

        modelBuilder.Entity<Inconveniente>(entity =>
        {
            entity.HasKey(e => e.IdInconveniente);

            entity.Property(e => e.IdInconveniente).HasColumnName("id_inconveniente");
            entity.Property(e => e.NombreInconveniente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_inconveniente");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona);

            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.ApellidoPersona)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellido_persona");
            entity.Property(e => e.Cedula)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contrasenia");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
            //Aca se pone para el unique
            entity.HasIndex(e => e.Usuario).IsUnique();
            entity.Property(e => e.FechaContratacion)
                .HasColumnType("date")
                .HasColumnName("fecha_contratacion");
            entity.Property(e => e.NombrePersona)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre_persona");
            entity.Property(e => e.Salario).HasColumnName("salario");
            entity.Property(e => e.TelefonoPersona)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("telefono_persona");
            entity.Property(e => e.TipoPersona)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('T')")
                .IsFixedLength()
                .HasColumnName("tipo_persona");
        });

        modelBuilder.Entity<Raza>(entity =>
        {
            entity.HasKey(e => e.IdRaza);

            entity.Property(e => e.IdRaza).HasColumnName("id_raza");
            entity.Property(e => e.NombreRaza)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_raza");
        });

        modelBuilder.Entity<ResInconveniente>(entity =>
        {
            entity.HasKey(e => new { e.FechaInconveniente, e.IdInconveniente, e.IdRes });

            entity.ToTable("Res_Inconveniente");

            entity.Property(e => e.FechaInconveniente)
                .HasColumnType("date")
                .HasColumnName("fecha_inconveniente");
            entity.Property(e => e.IdInconveniente).HasColumnName("id_inconveniente");
            entity.Property(e => e.IdRes).HasColumnName("id_res");
            entity.Property(e => e.DescripcionInconveniente)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("descripcion_inconveniente");
            entity.Property(e => e.DineroGastado).HasColumnName("dinero_gastado");

            entity.HasOne(d => d.IdInconvenienteNavigation).WithMany(p => p.ResInconvenientes)
                .HasForeignKey(d => d.IdInconveniente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Res_Inconveniente_Inconvenientes");

            entity.HasOne(d => d.IdResNavigation).WithMany(p => p.ResInconvenientes)
                .HasForeignKey(d => d.IdRes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Res_Inconveniente_Reses");
        });

        modelBuilder.Entity<ResRaza>(entity =>
        {
            entity.HasKey(e => new { e.IdRaza, e.IdRes });

            entity.ToTable("Res_Raza");

            entity.Property(e => e.IdRaza).HasColumnName("id_raza");
            entity.Property(e => e.IdRes).HasColumnName("id_res");
            entity.Property(e => e.PorcentajeRaza).HasColumnName("porcentaje_raza");

            entity.HasOne(d => d.IdRazaNavigation).WithMany(p => p.ResRazas)
                .HasForeignKey(d => d.IdRaza)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Res_Raza_Razas");

            entity.HasOne(d => d.IdResNavigation).WithMany(p => p.ResRazas)
                .HasForeignKey(d => d.IdRes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Res_Raza_Reses");
        });

        modelBuilder.Entity<Rese>(entity =>
        {
            entity.HasKey(e => e.IdRes);

            entity.Property(e => e.IdRes).HasColumnName("id_res");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.IdFinca).HasColumnName("id_finca");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.NombreRes)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_res");
            entity.Property(e => e.ValorVenta).HasColumnName("valor_venta");

            entity.HasOne(d => d.IdFincaNavigation).WithMany(p => p.Reses)
                .HasForeignKey(d => d.IdFinca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reses_Fincas");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Reses)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK_Reses_Ventas");
        });

        modelBuilder.Entity<TrabajadorFinca>(entity =>
        {
            entity.HasKey(e => new { e.IdTrabajador, e.IdFinca });

            entity.ToTable("Trabajador_Finca");

            entity.Property(e => e.IdTrabajador).HasColumnName("id_trabajador");
            entity.Property(e => e.IdFinca).HasColumnName("id_finca");
            entity.Property(e => e.EstadoTrabajador).HasColumnName("estado_trabajador");

            entity.HasOne(d => d.IdFincaNavigation).WithMany(p => p.TrabajadorFincas)
                .HasForeignKey(d => d.IdFinca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trabajador_Finca_Fincas");

            entity.HasOne(d => d.IdTrabajadorNavigation).WithMany(p => p.TrabajadorFincas)
                .HasForeignKey(d => d.IdTrabajador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trabajador_Finca_Personas");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta);

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("date")
                .HasColumnName("fecha_venta");
            entity.Property(e => e.IdComprador).HasColumnName("id_comprador");

            entity.HasOne(d => d.IdCompradorNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdComprador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Personas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

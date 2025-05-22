using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiProyectoDSII.Models;

public partial class TransporteFloresDbContext : DbContext
{
    public TransporteFloresDbContext()
    {
    }

    public TransporteFloresDbContext(DbContextOptions<TransporteFloresDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Conductore> Conductores { get; set; }

    public virtual DbSet<DetalleFacturacion> DetalleFacturacions { get; set; }

    public virtual DbSet<Direccione> Direcciones { get; set; }

    public virtual DbSet<Envio> Envios { get; set; }

    public virtual DbSet<Facturacion> Facturacions { get; set; }

    public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }

    public virtual DbSet<Ruta> Rutas { get; set; }

    public virtual DbSet<Unidade> Unidades { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdClientes).HasName("PK__Clientes__470BDBA0CCF276C4");

            entity.Property(e => e.IdClientes).HasColumnName("idClientes");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreCliente");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoCliente)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Conductore>(entity =>
        {
            entity.HasKey(e => e.IdConductores).HasName("PK__Conducto__D894A95F03E7B233");

            entity.Property(e => e.IdConductores).HasColumnName("idConductores");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdVehiculo).HasColumnName("idVehiculo");
            entity.Property(e => e.Licencia)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany(p => p.Conductores)
                .HasForeignKey(d => d.IdVehiculo)
                .HasConstraintName("FK__Conductor__idVeh__3B75D760");
        });

        modelBuilder.Entity<DetalleFacturacion>(entity =>
        {
            entity.HasKey(e => e.IdDetalleFacturacion).HasName("PK__DetalleF__F1D8C766D3DAE284");

            entity.ToTable("DetalleFacturacion");

            entity.Property(e => e.IdDetalleFacturacion).HasColumnName("idDetalleFacturacion");
            entity.Property(e => e.Detalle)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IdFacturacion).HasColumnName("idFacturacion");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdFacturacionNavigation).WithMany(p => p.DetalleFacturacions)
                .HasForeignKey(d => d.IdFacturacion)
                .HasConstraintName("FK__DetalleFa__idFac__4CA06362");
        });

        modelBuilder.Entity<Direccione>(entity =>
        {
            entity.HasKey(e => e.IdDirecciones).HasName("PK__Direccio__25D6A774218A6DC5");

            entity.Property(e => e.IdDirecciones).HasColumnName("idDirecciones");
            entity.Property(e => e.Direccion1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Direccion2)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Envio>(entity =>
        {
            entity.HasKey(e => e.IdEnvios).HasName("PK__Envíos__32D6B7E504BD2E70");

            entity.Property(e => e.IdEnvios).HasColumnName("idEnvios");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdRuta).HasColumnName("idRuta");
            entity.Property(e => e.Mercancia)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PesoTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VolumenTotal).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Envíos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Envíos__idClient__44FF419A");

            entity.HasOne(d => d.IdRutaNavigation).WithMany(p => p.Envíos)
                .HasForeignKey(d => d.IdRuta)
                .HasConstraintName("FK__Envíos__idRuta__45F365D3");
        });

        modelBuilder.Entity<Facturacion>(entity =>
        {
            entity.HasKey(e => e.IdFacturacion).HasName("PK__Facturac__2627C183F303C00F");

            entity.ToTable("Facturacion");

            entity.Property(e => e.IdFacturacion).HasColumnName("idFacturacion");
            entity.Property(e => e.EstadoPago)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdEnvio).HasColumnName("idEnvio");
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Facturacions)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Facturaci__idCli__48CFD27E");

            entity.HasOne(d => d.IdEnvioNavigation).WithMany(p => p.Facturacions)
                .HasForeignKey(d => d.IdEnvio)
                .HasConstraintName("FK__Facturaci__idEnv__49C3F6B7");
        });

        modelBuilder.Entity<Mantenimiento>(entity =>
        {
            entity.HasKey(e => e.IdMantenimientos).HasName("PK__Mantenim__09C696478AC01448");

            entity.Property(e => e.IdMantenimientos).HasColumnName("idMantenimientos");
            entity.Property(e => e.IdUnidad).HasColumnName("idUnidad");

            entity.HasOne(d => d.IdUnidadNavigation).WithMany(p => p.Mantenimientos)
                .HasForeignKey(d => d.IdUnidad)
                .HasConstraintName("FK__Mantenimi__idUni__4222D4EF");
        });

        modelBuilder.Entity<Ruta>(entity =>
        {
            entity.HasKey(e => e.IdRutas).HasName("PK__Rutas__F4B126249C1B5747");

            entity.Property(e => e.IdRutas).HasColumnName("idRutas");
            entity.Property(e => e.Destino)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Origen)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Unidade>(entity =>
        {
            entity.HasKey(e => e.IdUnidades).HasName("PK__Unidades__5C4AD0DB38D8EDBD");

            entity.Property(e => e.IdUnidades).HasColumnName("idUnidades");
            entity.Property(e => e.TipoUnidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Placa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Año);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.KilometrajeActual);
        });


        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuarios).HasName("PK__Usuarios__3940559AEA7AEDAC");

            entity.Property(e => e.IdUsuarios).HasColumnName("idUsuarios");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreUsuario");
            entity.Property(e => e.Rol)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

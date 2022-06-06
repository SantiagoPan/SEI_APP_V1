using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEI_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SEI_APP.Areas.Identity.Data
{
    public class SEI_Context : IdentityDbContext<ApplicationUser>
    {
        public SEI_Context(DbContextOptions<SEI_Context> options)
            : base(options)
        {

        }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Barrio> Barrio { get; set; }
        public DbSet<CaracterizacionProducto> CaracterizacionProducto { get; set; }
        public DbSet<CaracterizacionServicio> CaracterizacionServicio { get; set; }
        public DbSet<CategoriaProducto> CategoriaProducto { get; set; }
        public DbSet<CategoriaServicio> CategoriaServicio { get; set; }
        public DbSet<EstadoProductoServicio> EstadoProductoServicio { get; set; }
        public DbSet<Localizacion> Localizacion { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<PublicacionProductoServicio> PublicacionProductoServicio { get; set; }
        public DbSet<TipoCategoriaProducto> TipoCategoriaProducto { get; set; }
        public DbSet<TipoCategoriaServicio> TipoCategoriaServicio { get; set; }
        public DbSet<TipoGarantiaProducto> TipoGarantiaProducto { get; set; }
        public DbSet<TipoMaterialProducto> TipoMaterialProducto { get; set; }
        public DbSet<TipoProducto> TipoProducto { get; set; }
        public DbSet<TipoServicio> TipoServicio { get; set; }
        public DbSet<TipoDocumentoIdentidad> TipoDocumentoIdentidad { get; set; }
        public DbSet<EstadoVenta> EstadoVenta { get; set; }
        public DbSet<TipoPago> TipoPago { get; set; }
        public DbSet<CalificacionProducto> CalificacionProducto { get; set; }
        public DbSet<CalificacionServicio> CalificacionServicio { get; set; }
        public DbSet<Envios> Envios { get; set; }
        public DbSet<MotivoFinalizacionServicio> MotivoFinalizacionServicio { get; set; }
        public DbSet<VentasServicios> VentasServicios { get; set; }
        public DbSet<VentasProductos> VentasProductos { get; set; }
        public DbSet<AdjuntosVentasServicios> AdjuntosVentasServicios { get; set; }
        public DbSet<MensajesVentaServicio> MensajesVentaServicio { get; set; }
        public DbSet<NotificacionMasiva> NotificacionMasiva { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Municipio>(b =>
            {
                b.HasKey(e => e.IdMunicipio);
                b.Property(e => e.IdMunicipio).ValueGeneratedOnAdd();
            });

            builder.Entity<Localizacion>(b =>
            {
                b.HasKey(e => e.IdLocalizacion);
                b.Property(e => e.IdLocalizacion).ValueGeneratedOnAdd();
            });
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace SEI_APP.Areas.Identity.Data
{
    public class Envios
    {
		[Key]
		[Required]
		public int IdEnvios { get; set; }
		[Required]
		public string NombreCompradors { get; set; }
		[Required]
		public string Telefono { get; set; }
		[Required]
		public string CorreoElectronico{ get; set; } 
		public string DireccionEnvio { get; set; }
		public string DatosAdicionales { get; set; }
		public string EnvioGratis { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public string UsuarioModificacion{ get; set; }
		public string UsuarioCreacion { get; set; }
		[Required]
		public int IdVentasProductos { get; set; }
		[Required]
		public string IdUsuarioComprador { get; set; }
		[Required]
		public int IdMunicipio { get; set; }
		[Required]
		public int IdBarrio { get; set; }

	}
}

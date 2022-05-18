using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEI_APP.Areas.Identity.Data
{
    public class VentasProductos
    {
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int IdVentasProductos { get; set; }
		[Required]
		public double UnidadesCompradas { get; set; }
		[Required]
		public double ValorPorUnidad { get; set; }
		[Required]
		public double ValorTotal { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public string UsuarioModificacion { get; set; }
		public string UsuarioCreacion { get; set; }
		[Required]
		public int IdProducto { get; set; }
		[Required]
		public string IdUsuarioComprador { get; set; }
		[Required]
		public string IdUsuarioVendedor { get; set; }
		[Required]
		public int IdTipoPago{ get; set; }
		[Required]
		public int IdEstadoVenta { get; set; } 

	}
}

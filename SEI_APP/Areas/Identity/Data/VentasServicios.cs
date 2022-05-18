using System;
using System.ComponentModel.DataAnnotations;

namespace SEI_APP.Areas.Identity.Data
{
    public class VentasServicios
    {
        [Key]
        public int IdVentasServicios { get; set; }
        public double ValoTotal { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        [Required]
        public int IdServicio { get; set; }
        [Required]
        public string IdUsuarioComprador { get; set; }
        [Required]
        public string IdUsuarioVendedor { get; set; }
        [Required]
        public int IdTipoPago { get; set; }
        [Required]
        public int IdEstadoVenta { get; set; }
        [Required] 
        public int IdMotivoFinalizacionServicio { get; set; }


    }
}

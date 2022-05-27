using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEI_APP.Areas.Identity.Data
{
    public class MensajesVentaServicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMensajesVentaServicio { get; set; }
        public string Mensaje { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioVendedor { get; set; }
        public string UsuarioComprador { get; set; }
        [Required]
        public int IdVentasServicios { get; set; }
        public int? IdMensajeRespuesta { get; set; }
    }
}

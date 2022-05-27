using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEI_APP.Areas.Identity.Data
{
    public class AdjuntosVentasServicios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAdjuntosVentasServicios { get; set; }
        public string NombreDocumento { get; set; }
        public string Documento { get; set; }
        public int Activo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        [Required]
        public int IdVentasServicios { get; set; }
    }
}

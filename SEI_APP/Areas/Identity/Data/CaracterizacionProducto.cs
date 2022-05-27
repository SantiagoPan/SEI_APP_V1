using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEI_APP.Areas.Identity.Data
{
    public class CaracterizacionProducto
    {
        [Key]
        public int IdCaracterizacionProducto { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Condicion { get; set; }
        public string Ancho { get; set; }
        public string Alto { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        [Required]
        [ForeignKey("IdTipoMaterialProducto")]
        public int IdTipoMaterialProducto { get; set; }
        [Required]
        [ForeignKey("IdTipoGarantiaProducto")]
        public int IdTipoGarantiaProducto { get; set; }
        public string EnvioGratis { get; set; }
    }
}

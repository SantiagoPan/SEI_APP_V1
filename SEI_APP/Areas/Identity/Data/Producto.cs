using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEI_APP.Areas.Identity.Data
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string NombreProducto { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        [Required]
        public double CostoProducto { get; set; }
        [Required]
        public double UnidadesProducto { get; set; }
        [Required]
        [ForeignKey("IdTipoProducto")]
        public int IdTipoProducto { get; set; }
        [Required]
        [ForeignKey("IdCaracterizacionProducto")]
        public int IdCaracterizacionProducto { get; set; }
        [Required]
        [ForeignKey("IdEstadoProductoServicio")]
        public int IdEstadoProductoServicio { get; set; }
        [Required]
        [ForeignKey("IdLocalizacion")]
        public int IdLocalizacion{ get; set; }
        public string Imagen { get; set; }

    }
}

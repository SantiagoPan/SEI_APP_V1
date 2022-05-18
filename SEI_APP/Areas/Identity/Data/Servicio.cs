using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEI_APP.Areas.Identity.Data
{
    public class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdServicio { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string NombreServicio { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        [Required]
        public double CostoServicio { get; set; }
        [ForeignKey("IdTipoServicio")]
        public int IdTipoServicio { get; set; }
        [Required]
        [ForeignKey("IdCaracterizacionServicio")]
        public int IdCaracterizacionServicio { get; set; }
        [Required]
        [ForeignKey("IdEstadoProductoServicio")]
        public int IdEstadoProductoServicio { get; set; }
        [Required]
        [ForeignKey("IdLocalizacion")]
        public int IdLocalizacion { get; set; }
        public string Imagen { get; set; }
    }
}

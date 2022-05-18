using System;
using System.ComponentModel.DataAnnotations;

namespace SEI_APP.Areas.Identity.Data
{
    public class EstadoProductoServicio
    {
        [Key]
        public string IdEstadoProductoServicio { get; set; }
        [Required]
        public int Activo{ get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string UsuarioModificacion { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }
        [Required]
        public string UsuarioCreacion { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace SEI_APP.Areas.Identity.Data
{
    public class PublicacionProductoServicio
    {
        [Key]
        public int IdPublicacionProductoServicio { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }
        [Required]
        public string UsuarioModificacion { get; set; }
        [Required]
        public string UsuarioCreacion{ get; set; }


    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace SEI_APP.Areas.Identity.Data
{
    public class CaracterizacionServicio
    {
        [Key]
        public int IdCaracterizacionServicio { get; set; }
        [Required]
        public int Experiencia { get; set; }
        public string Incluye{ get; set; }
        public string NoIncluye{ get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }

        
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEI_APP.Areas.Identity.Data
{
    public class CalificacionServicio
    {
        [Key]
        public int IdCalificacionServicio { get; set; }
        public int Calificacion { get; set; }
        public string Observacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        [Required]
        [ForeignKey("IdUsuarioCliente")]
        public string IdUsuarioCliente { get; set; }
        [Required]
        [ForeignKey("IdUsuarioCalificado")]
        public string IdUsuarioCalificado { get; set; }
        [Required]
        [ForeignKey("IdProducto")]
        public int IdServicio { get; set; }
    }
}

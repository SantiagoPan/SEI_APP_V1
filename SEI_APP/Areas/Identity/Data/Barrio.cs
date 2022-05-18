using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEI_APP.Areas.Identity.Data
{
    public class Barrio
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBarrio { get; set; }
        public string CodigoDane { get; set; }
        public string Nombre { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        public DateTime FechaCreacion { get; set; }
        [Required]
        public int IdMunicipio { get; set; }
    }
}

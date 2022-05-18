using System;
using System.ComponentModel.DataAnnotations;

namespace SEI_APP.Areas.Identity.Data
{
    public class Departamento
    {
        [Key]
        public int IdDepartamento { get; set; }
        public string CodigoDane { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}

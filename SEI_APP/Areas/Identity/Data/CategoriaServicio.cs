using System;
using System.ComponentModel.DataAnnotations;

namespace SEI_APP.Areas.Identity.Data
{
    public class CategoriaServicio
    {
        [Key]
        public int IdCategoriaServicio { get; set; }
        public string Nombre { get; set; }
        public int Activo { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public int IdTipoCategoriaServicio { get; set; }
    }
}

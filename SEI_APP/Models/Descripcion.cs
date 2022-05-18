using System.ComponentModel.DataAnnotations;

namespace SEI_APP.Models
{
    public class Descripcion
    {

        [Required]
        public string NombreServicio { get; set; }
        [Required]
        public string CosotoServicio { get; set; }
        [Required]
        public int IdTipoServicio { get; set; }
    }
}

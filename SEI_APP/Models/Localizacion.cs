using System.ComponentModel.DataAnnotations;

namespace SEI_APP.Models
{
    public class Localizacion
    {
        [Required]
        public string Dirreccion { get; set; }
        [Required]
        public string CodigoPostal { get; set; }
        [Required]
        public string DatosAdicionales { get; set; }
        [Required]
        public int IdMunicipio { get; set; }
        [Required]
        public int  IdBarrio { get; set; }
    }
}

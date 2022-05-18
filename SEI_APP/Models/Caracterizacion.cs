using System.ComponentModel.DataAnnotations;

namespace SEI_APP.Models
{
    public class Caracterizacion
    {
        [Required]
        public string Experiencia { get; set; }
        [Required]
        public string Incluye   { get; set; }
        [Required]
        public string NoIncluye { get; set; }
    }
}

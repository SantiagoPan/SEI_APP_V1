using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEI_APP.Areas.Identity.Data
{
    public class Localizacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLocalizacion { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal{ get; set; }
        public string DatosAdicionales { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public int IdMunicipio { get; set; }
        public int IdBarrio { get; set; }
        public string Telefono { get; set; }
        public string TelefonoOpc { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
    }
}

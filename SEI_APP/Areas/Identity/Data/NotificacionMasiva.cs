using System;
using System.ComponentModel.DataAnnotations;

namespace SEI_APP.Areas.Identity.Data
{
    public class NotificacionMasiva
    {
        [Key]
        [Required]
        public int IdNotificacionMasiva { get; set; }
        [Required]
        public string Mensaje { get; set; }
        [Required]
        public DateTime FechaMensaje { get; set; }
    }
}

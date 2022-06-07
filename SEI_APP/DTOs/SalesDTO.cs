using System.Collections.Generic;

namespace SEI_APP.DTOs
{
    public class SalesDTO
    {
        public class Ventas
        {
            public string Nombre{ get; set; }
            public double Precio { get; set; }
            public string FechaVenta { get; set; }
            public string NombreComprador { get; set; }
            public string Tipo { get; set; }
            public string EstadoVenta { get; set; }
        }

        public class Mensajes
        {
            public int IdMensaje { get; set; }
            public string Mensaje { get; set; }
            public string Respuesta { get; set; }
            public string Servicio { get; set; }
            public string Fecha { get; set; }
            public string NombreVendedor { get; set; }

        }
    }
}

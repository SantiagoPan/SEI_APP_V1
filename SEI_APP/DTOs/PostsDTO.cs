using System.Collections.Generic;

namespace SEI_APP.DTOs
{
    public class PostsDTO

    {
        public class Publicaciones
        {
            public int Id { get; set; }
            public double Precio { get; set; }
            public string Nombre { get; set; }
            public string FechaPublicacion { get; set; }
            public string Tipo { get; set; }
            public string Estado { get; set; }
        }

        public class Servicios
        {
            public int IdServicio { get; set; }
            public double Precio { get; set; }
            public string NombreServicio { get; set; }
            public string FechaPublicacion { get; set; }
            public string Servicio { get; set; }
            public string Fecha { get; set; }
            public string NombreComprador { get; set; }

        }
    }
}

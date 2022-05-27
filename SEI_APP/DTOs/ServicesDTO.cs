using System;
using System.ComponentModel.DataAnnotations;

namespace SEI_APP.DTOs
{
    public class ServicesDTO
    {
        public Service servicio { get; set; }
        public Localization localizacion { get; set; }
        public Characterization caracterizacion { get; set; }

        public class Service
        {
            public int IdServicio { get; set; }
            public string Descripcion { get; set; }
            public string IdUsuario { get; set; }
            public string NombreServicio { get; set; }
            public DateTime? FechaModificacion { get; set; }
            public string UsuarioModificacion { get; set; }
            public string UsuarioCreacion { get; set; }
            public DateTime? FechaCreacion { get; set; }
            public string AplicaConvenio { get; set; }
            public float CostoServicio { get; set; }
            public int IdTipoServicio { get; set; }
            public int IdCaracterizacionServicio { get; set; }
            public int IdEstadoProductoServicio { get; set; }
            public int IdLocalizacion { get; set; }
            public string Imagen { get; set; }
        }

        public class Characterization
        {
            public int Experiencia { get; set; }
            public string Incluye { get; set; }
            public string NoIncluye { get; set; }
            public DateTime? FechaCreacion { get; set; }
            public DateTime? FechaModificacion { get; set; }
            public string UsuarioModificacion { get; set; }
            public string UsuarioCreacion { get; set; }
        }

        public class Localization
        {
            public string Direccion { get; set; }
            public string CodigoPostal { get; set; }
            public string DatosAdicionales { get; set; }
            public string FechaCreacion { get; set; }
            public string FechaModificacion { get; set; }
            public string UsuarioModificacion { get; set; }
            public string UsuarioCreacion { get; set; }
            public int IdMunicipio { get; set; }
            public int IdBarrio { get; set; }
            public string Telefono { get; set; }
            public string TelefonOpc { get; set; }
            public string Email { get; set; }
            public string WebSite { get; set; }

        }

    }
    public class ServicesDTOAttachments
    {
        public int IdVentaServicio { get; set; }
        public string IdUsuario { get; set; }
        public string fileName { get; set; }
        public string url { get; set; }
    }

    public class ServicesDTOMessage
    {
        public int IdVentaServicio { get; set; }
        public int IdMensajesVentaServicio { get; set; }
        public string IdUsuario { get; set; }
        public string Mensaje { get; set; }
    }
    public class ServicesDTOHire
    {
        public int IdServicio { get; set; }
        public int IdTipoPago { get; set; }
        public string IdUsuario { get; set; }
    }

    public class ServicesDTOFinish
    {
        public int IdServicio { get; set; }
        public int IdVentaServicio { get; set; }
        public int IdMotivoFinalizacion { get; set; }
        public string IdUsuarioComprador { get; set; }
        public int Calificacion { get; set; }
        public string Observacion { get; set; }
    }

    public class ServicesDTOResponse
    {
        public Service servicio { get; set; }
        
        public class Service
        {
            public int IdVentaServicio { get; set; }
            public int IdServicio { get; set; }
            public string Descripcion { get; set; }
            public string NombreServicio { get; set; }
            public string EstadoVenta { get; set; }
            public string MotivoFinalizacion { get; set; }
            public double CostoServicio { get; set; }
            public string Calificacion { get; set; }
            public string AplicaConvenio { get; set; }
            public string NombrePrestador { get; set; }
            public string Experiencia { get; set; }
            public string NombreBanco { get; set; }
            public string NumeroCuenta { get; set; }
            public string TipoCuenta { get; set; }
            public string Telefono { get; set; }
            public string TipoServicio { get; set; }
            public string Imagen { get; set; }
            public string[] Adjuntos { get; set; }
            public int IdEstadoProductoServicio { get; set; }
            public Caracteristicas Caracteristicas { get; set; }
            public Localizacion localizacion { get; set; }
        }
        public class Caracteristicas
        {
            public string Experiencia { get; set; }
            public string Incluye { get; set; }
            public string NoIncluye { get; set; }
            public string Nombre { get; set; }
            public string Telefono { get; set; }
            public string Calificacion { get; set; }
            public string Banco { get; set; }
            public string TipoCuenta { get; set; }
            public string NumeroCuenta { get; set; }
        }

        public class Localizacion
        {
            public string Departmento { get; set; }
            public string Ciudad { get; set; }
            public string Barrio { get; set; }
        }
    }
}

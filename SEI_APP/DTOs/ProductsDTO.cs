using System.ComponentModel.DataAnnotations;
using System;

namespace SEI_APP.DTOs
{ 
        public class ProductsDTO
        {
            public Product producto { get; set; }
            public Localization localizacion { get; set; }
            public Characterization caracterizacion { get; set; }

            public class Product
            {
                public int IdProducto { get; set; }
                public string Descripcion { get; set; }
                public string NombreProducto{ get; set; }
                public DateTime? FechaModificacion { get; set; }
                public string UsuarioModificacion { get; set; }
                public string UsuarioCreacion { get; set; }
                public DateTime? FechaCreacion { get; set; }
                public float CostoProducto { get; set; }
                public float Unidades { get; set; }
                public int IdTipoProducto { get; set; }
                public int IdCaracterizacionProducto { get; set; }
                public int IdEstadoProductoServicio { get; set; }
                public int IdLocalizacion { get; set; }
                public string Imagen { get; set; }
                public string IdUsuario { get; set; }
        }

            public class Characterization
            {
                public string Marca { get; set; }
                public string Modelo { get; set; }
                public string Condicion { get; set; }
                public string Ancho{ get; set; }
                public string Alto { get; set; }
                public DateTime? FechaCreacion { get; set; }
                public DateTime? FechaModificacion { get; set; }
                public string UsuarioModificacion { get; set; }
                public string UsuarioCreacion { get; set; }
                public int IdTipoMaterialProducto { get; set; }
                public int IdTipoGarantiaProducto { get; set; }
                public string EnvioGratis { get; set; }
        }
        

            public class Localization
            {
                public string Direccion { get; set; }
                public string Telefono { get; set; }
                public string TelefonOpc { get; set; }
                public string Email { get; set; }
                public string WebSite { get; set; }
                public string CodigoPostal { get; set; }
                public string DatosAdicionales { get; set; }
                public string FechaCreacion { get; set; }
                public string FechaModificacion { get; set; }
                public string UsuarioModificacion { get; set; }
                public string UsuarioCreacion { get; set; }
                public int IdMunicipio { get; set; }
                public int IdBarrio { get; set; }
            }

        }
    public class ProductsDTOQualify
    {
        public string IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public int Calificacion { get; set; }
        public string Observacion { get; set; }

    }

    public class ProductsDTOBuy
    {
        public Producto producto { get; set; }
        public Envio envio { get; set; }
        public class Producto
        {
            public int IdProducto { get; set; }
            public int IdTipoPago { get; set; }
            public int UnidadesCompradas { get; set; }
            public string IdUsuarioComprador { get; set; }
        }

        public class Envio
        {
            public string NombreCompradors { get; set; }
            public string Telefono { get; set; }
            public string CorreoElectronico { get; set; }
            public string DireccionEnvio { get; set; }
            public string DatosAdicionales { get; set; }
            public int IdMunicipio { get; set; }
            public int IdBarrio { get; set; }
            public int IdVentasProductos { get; set; }

        }
    }

    public class ProductsDTOResponse
    {
        public Producto producto { get; set; }
        public class Producto
        {
            public int IdProducto { get; set; }
            public string Descripcion { get; set; }
            public string EstadoVenta { get; set; }
            public string NombreProducto { get; set; }
            public string Imagen { get; set; }
            public double CostoProducto { get; set; }
            public double Unidades { get; set; }
            public string TipoPago { get; set; }
            public string NombreVendedor { get; set; }
            public string FechaCompra { get; set; }
            public string FechaPublicacion { get; set; }
            public string EstadoPublicacion { get; set; }
            public string NombreTipoProducto { get; set; }
            public string Condicion { get; set; }
            public string Marca { get; set; }
            public double ValorTotalCompra { get; set; }
            public int Calificacion { get; set; }
            public string EstrellasCalificacion { get; set; }
            public Caracteristicas Caracteristicas { get; set; }
            public int EstadoProductoServicio { get; set; }
            public Localizacion Localizacion { get; set; }

        }
        public class Caracteristicas
        {
            public string Marca { get; set; }
            public string Modelo { get; set; }
            public string Ancho { get; set; }
            public string Alto { get; set; }
            public string Condicion { get; set; }
            public string Material { get; set; }
            public string Garantia { get; set; }
            public string EnvioGratis { get; set; }
        }
        public class Localizacion
        {
            public string Departmento { get; set; }
            public string Ciudad { get; set; }
            public string Barrio { get; set; }
        }

    }
    public class BuyProductsDTO
    {
        public Producto producto { get; set; }
        public class Producto
        {
            public int IdProducto { get; set; }
            public int UnidadesCompradas { get; set; }
            public float ValorUnidad { get; set; }
            public float ValorTotal { get; set; }
            public int IdTipoPago { get; set; }

        }
    }
}



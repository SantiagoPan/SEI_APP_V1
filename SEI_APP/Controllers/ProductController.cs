using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SEI_APP.Areas.Identity.Data;
using SEI_APP.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEI_APP.Controllers
{
   
        [ApiController]
        [Route("[controller]")]
        public class ProductController : ControllerBase
        {
            private readonly IConfiguration configuration;
            private readonly SEI_Context contextDB;
            public ProductController(IConfiguration configuration, SEI_Context contextDB)
            {
                this.configuration = configuration;
                this.contextDB = contextDB;

            }

            [HttpGet("getDepartments")]
            public async Task<ActionResult> GetDepartments()
            {
                try
                {
                    var departments = new List<Departamento>();
                    departments = await contextDB.Departamento.ToListAsync();
                    if (departments.Count == 0)
                    {
                        return BadRequest();
                    }
                    var optionSelect = new Departamento();
                    optionSelect.IdDepartamento = departments.Count + 1;
                    optionSelect.Nombre = "Seleccione";
                    optionSelect.CodigoDane = "1";
                    departments.Add(optionSelect);
                    List<Departamento> SortedList = departments.OrderByDescending(o => o.IdDepartamento).ToList();
                    JsonResult result = new JsonResult(SortedList);
                    result.Value = SortedList;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return result;
                }
                catch (Exception ex)
                {
                    var erd = ex.Message;
                    throw;
                }
            }
        [HttpGet("getPostsById")]
        public async Task<JsonResult> GetPostsById(string idUser)
        {
            string strSearch = string.Empty;
            JsonResult result = new JsonResult(strSearch);
            try
            {
                var servicios = new List<Servicio>();
                var productos = new List<Producto>();
                var publicaciones = new List<PostsDTO.Publicaciones>();

                servicios = await contextDB.Servicio.Where(x => x.UsuarioCreacion == idUser).ToListAsync();
                productos = await contextDB.Producto.Where(x => x.UsuarioCreacion == idUser).ToListAsync();
                if (servicios.Count != 0)
                {
                    foreach (var service in servicios)
                    {
                        var postServ = new PostsDTO.Publicaciones();
                        var serv = await contextDB.Servicio.Where(x => x.IdServicio == service.IdServicio).FirstOrDefaultAsync();
                        postServ.Id = serv.IdServicio;
                        postServ.Nombre = serv.NombreServicio;
                        postServ.Precio = service.CostoServicio;
                        postServ.FechaPublicacion = service.FechaCreacion.ToString();
                        postServ.Tipo = "Servicio";
                        postServ.Estado = await contextDB.EstadoProductoServicio.Where(x => x.IdEstadoProductoServicio == service.IdEstadoProductoServicio).Select(x => x.Nombre).FirstOrDefaultAsync();

                        publicaciones.Add(postServ);
                    }
                }

                if (productos.Count != 0)
                {
                    foreach (var product in productos)
                    {
                        var postProd = new PostsDTO.Publicaciones();
                        var prod = await contextDB.Producto.Where(x => x.IdProducto == product.IdProducto).FirstOrDefaultAsync();
                        postProd.Id = prod.IdProducto;
                        postProd.Nombre = prod.NombreProducto;
                        postProd.Precio = product.CostoProducto;
                        postProd.FechaPublicacion = product.FechaCreacion.ToString();
                        postProd.Tipo = "Producto";
                        postProd.Estado = await contextDB.EstadoProductoServicio.Where(x => x.IdEstadoProductoServicio == product.IdEstadoProductoServicio).Select(x => x.Nombre).FirstOrDefaultAsync();

                        publicaciones.Add(postProd);
                    }
                }
                result.Value = publicaciones;
                result.ContentType = "application/json";
                result.StatusCode = 200;

                return result;

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                result.Value = err;
            }
            result.ContentType = "application/json";
            result.StatusCode = 400;

            return result;
        }

        [HttpGet("getProductsByUser")]
        public async Task<ActionResult> GetProductsByUser(string idUser)
        {
            try
            {
                var listaProductos = new List<VentasProductos>();
                var productos = new List<ProductsDTOResponse>();
                JsonResult result = new JsonResult(listaProductos);
                listaProductos = await contextDB.VentasProductos.Where(x=>x.IdUsuarioComprador == idUser).ToListAsync();
                if (listaProductos.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return NoContent();
                }
                foreach (var item in listaProductos)
                {
                    var prodInfo = new ProductsDTOResponse();
                    var prod = new ProductsDTOResponse.Producto();
                    var productoC = await contextDB.Producto.Where(x =>x.IdProducto == item.IdProducto).FirstOrDefaultAsync();
                    var caracterizacionProd = await contextDB.CaracterizacionProducto.Where(x => x.IdCaracterizacionProducto == productoC.IdCaracterizacionProducto).FirstOrDefaultAsync();
                    var userInfo = await contextDB.Users.Where(x => x.Id == productoC.UsuarioCreacion).FirstOrDefaultAsync();
                    var calificado = await contextDB.CalificacionProducto.Where(x => x.IdProducto == item.IdProducto && x.IdUsuarioCliente == idUser).ToListAsync();
                    prod.NombreProducto = productoC.NombreProducto;
                    prod.CostoProducto = productoC.CostoProducto;
                    prod.Imagen = productoC.Imagen;
                    prod.IdProducto = productoC.IdProducto;
                    prod.Condicion = caracterizacionProd.Condicion;
                    prod.FechaCompra = item.FechaCreacion.ToString();
                    prod.Calificacion = calificado.Count();
                    prod.Unidades = item.UnidadesCompradas;
                    prod.ValorTotalCompra = item.ValorTotal;
                    prod.Marca = caracterizacionProd.Marca;
                    prod.TipoPago = await contextDB.TipoPago.Where(x => x.IdTipoPago == item.IdTipoPago).Select(x => x.Nombre).FirstOrDefaultAsync();
                    prod.NombreVendedor = userInfo.Name + " " + userInfo.Lastname;
                   
                    prodInfo.producto = prod;
                    productos.Add(prodInfo);
                }
                

                result.Value = productos;
                result.ContentType = "application/json";
                result.StatusCode = 200;
                return result;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }
        }


        [HttpGet("getTypeMaterialProduct")]
        public async Task<ActionResult> GetTypeMaterialProduct()
        {
            try
            {
                var tipoMaterialProducto = new List<TipoMaterialProducto>();
                JsonResult result = new JsonResult(tipoMaterialProducto);
                tipoMaterialProducto = await contextDB.TipoMaterialProducto.ToListAsync();
                if (tipoMaterialProducto.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return NoContent();
                }
                var optionSelect = new TipoMaterialProducto();
                optionSelect.IdTipoMaterialProducto = tipoMaterialProducto.Count + 1;
                optionSelect.Nombre = "Seleccione";
                tipoMaterialProducto.Add(optionSelect);
                List<TipoMaterialProducto> SortedList = tipoMaterialProducto.OrderByDescending(o => o.IdTipoMaterialProducto).ToList();
                result.Value = SortedList;
                result.ContentType = "application/json";
                result.StatusCode = 200;
                return result;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }
        }

        [HttpGet("getCategoryTypeProduct")]
        public async Task<ActionResult> GetCategoryTypeProduct()
        {
            try
            {
                var tipoCategoriasProducto = new List<TipoCategoriaProducto>();
                JsonResult result = new JsonResult(tipoCategoriasProducto);
                tipoCategoriasProducto = await contextDB.TipoCategoriaProducto.ToListAsync();
                if (tipoCategoriasProducto.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return NoContent();
                }
                var optionSelect = new TipoCategoriaProducto();
                optionSelect.IdTipoCategoriaProducto = tipoCategoriasProducto.Count + 1;
                optionSelect.Nombre = "Seleccione";
                tipoCategoriasProducto.Add(optionSelect);
                List<TipoCategoriaProducto> SortedList = tipoCategoriasProducto.OrderByDescending(o => o.IdTipoCategoriaProducto).ToList();
                result.Value = SortedList;
                result.ContentType = "application/json";
                result.StatusCode = 200;
                return result;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }
        }

        [HttpGet("getTypePayment")]
        public async Task<ActionResult> GetTypePayment()
        {
            try
            {
                var tipoPago = new List<TipoPago>();
                JsonResult result = new JsonResult(tipoPago);
                tipoPago = await contextDB.TipoPago.ToListAsync();
                if (tipoPago.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;

                    return NoContent();
                }
                var optionSelect = new TipoPago();
                optionSelect.IdTipoPago = tipoPago.Count + 1;
                optionSelect.Nombre = "Seleccione";
                tipoPago.Add(optionSelect);
                List<TipoPago> SortedList = tipoPago.OrderByDescending(o => o.IdTipoPago).ToList();
                result.Value = SortedList;
                result.ContentType = "application/json";
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }
        }

        [HttpGet("updateStatusProduct")]
        public async Task<JsonResult> UpdateStatusProduct(int idProducto, int idEstadoProducto)
        {
            string strSearch = string.Empty;
            JsonResult result = new JsonResult(strSearch);
            try
            {
                var producto = await contextDB.Producto.Where(x => x.IdProducto == idProducto).FirstOrDefaultAsync();
                if (idEstadoProducto == 3)
                {
                    producto.IdEstadoProductoServicio = 3;
                    contextDB.SaveChanges();
                    result.Value = "Registro Actualizado Correctamente";
                    result.ContentType = "application/json";
                    result.StatusCode = 200;

                    return result;
                }
                if (producto.IdEstadoProductoServicio == 1)
                {
                    producto.IdEstadoProductoServicio = 2;
                    contextDB.SaveChanges();
                }
                else
                {
                    producto.IdEstadoProductoServicio = 1;
                    contextDB.SaveChanges();
                }
                result.Value = "Registro Actualizado Correctamente";
                result.ContentType = "application/json";
                result.StatusCode = 200;

                return result;

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                result.Value = err;
            }
            result.ContentType = "application/json";
            result.StatusCode = 400;

            return result;
        }

        [HttpGet("getProductCategory")]
        public async Task<ActionResult> GetProductCategory(int idTipoCategoriaProducto)
        {
            try
            {
                var productCategory = new List<CategoriaProducto>();
                JsonResult result = new JsonResult(productCategory);
                productCategory = await contextDB.CategoriaProducto.Where(x => x.IdTipoCategoriaProducto == idTipoCategoriaProducto).ToListAsync();
                if (productCategory.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;

                    return NoContent();
                }
                var optionSelect = new CategoriaProducto();
                int maxId = productCategory.Max(u => u.IdCategoriaProducto);
                optionSelect.IdCategoriaProducto = maxId + 1;
                optionSelect.Nombre = "Seleccione";
                productCategory.Add(optionSelect);
                List<CategoriaProducto> SortedList = productCategory.OrderByDescending(o => o.IdCategoriaProducto).ToList();
                result.Value = SortedList;
                result.ContentType = "application/json";
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                var erd = ex.Message;
                throw;
            }
        }

        [HttpGet("getProductType")]
        public async Task<ActionResult> GetServiceType(int idCategoriaProducto)
        {
            try
            {
                var productType = new List<TipoProducto>();
                JsonResult result = new JsonResult(productType);
                productType = await contextDB.TipoProducto.Where(x => x.IdCategoriaProducto == idCategoriaProducto).ToListAsync();
                if (productType.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;

                    return NoContent();
                }
                var optionSelect = new TipoProducto();
                int maxId = productType.Max(u => u.IdTipoProducto);
                optionSelect.IdTipoProducto = maxId +1;
                optionSelect.NombreTiipoProducto = "Seleccione";
                productType.Add(optionSelect);
                List<TipoProducto> SortedList = productType.OrderByDescending(o => o.IdTipoProducto).ToList();
                result.Value = SortedList;
                result.ContentType = "application/json";
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                var erd = ex.Message;
                throw;
            }
        }

        [HttpGet("getProductsPostedByUser")]
        public async Task<JsonResult> GetProducts(string idUser)
        {
            string strSearch = string.Empty;
            JsonResult result = new JsonResult(strSearch);
            try
            {
                var products = new List<Producto>();
                var productsList = new List<ProductsDTOResponse>();
                products = await contextDB.Producto.Where(x=>x.UsuarioCreacion == idUser && x.IdEstadoProductoServicio != 3).ToListAsync();
                foreach (var item in products)
                {
                    var productInfo = new ProductsDTOResponse();
                    var prod = new ProductsDTOResponse.Producto();
                    var loc = new ProductsDTOResponse.Localizacion();
                    var cac = new ProductsDTOResponse.Caracteristicas();
                    var cal = 0;
                    prod.IdProducto = item.IdProducto;
                    prod.CostoProducto = item.CostoProducto;
                    prod.NombreProducto = item.NombreProducto;
                    prod.Descripcion = item.Descripcion;
                    prod.EstadoProductoServicio = item.IdEstadoProductoServicio;
                    cac = await GetInfoCaracteristicas(item.IdCaracterizacionProducto);
                    loc = await GetInfoLocalizacion(item.IdLocalizacion);
                    cal = await GetInfoCalificacion(item.IdProducto);
                    string stars = "";
                    if (cal == 0)
                    {
                        stars = "Sin calificaciones.";
                    }
                    else
                    {
                        for (int i = 0; i < cal; i++)
                        {
                            stars += "★ ";
                        }
                    }

                    prod.EstrellasCalificacion = stars;
                    prod.NombreTipoProducto = await contextDB.TipoProducto.Where(x => x.IdTipoProducto == item.IdTipoProducto).Select(x => x.NombreTiipoProducto).FirstOrDefaultAsync();
                    prod.Localizacion = loc;
                    prod.Caracteristicas = cac;
                    prod.Calificacion = cal;
                    prod.Unidades = item.UnidadesProducto;
                    prod.Imagen = item.Imagen;
                    prod.FechaPublicacion = item.FechaCreacion.ToString();
                    prod.EstadoPublicacion = await contextDB.EstadoProductoServicio.Where(x => x.IdEstadoProductoServicio == item.IdEstadoProductoServicio).Select(x => x.Nombre).FirstOrDefaultAsync();
                    productInfo.producto = prod;
                    productsList.Add(productInfo);
                }

                result.Value = productsList;
                result.ContentType = "application/json";
                result.StatusCode = 200;

                return result;

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                result.Value = err;
            }
            result.ContentType = "application/json";
            result.StatusCode = 400;

            return result;
        }

        [HttpGet("getProducts")]
        public async Task<JsonResult> GetProducts()
        {
            string strSearch = string.Empty;
            JsonResult result = new JsonResult(strSearch);
            try
            {
                var products = new List<Producto>();
                var productsList = new List<ProductsDTOResponse>();
                var idUser = await contextDB.Users.FirstOrDefaultAsync();
                products = await contextDB.Producto.Where(x=>x.IdEstadoProductoServicio == 1).ToListAsync();
                foreach (var item in products)
                {
                    var productInfo = new ProductsDTOResponse();
                    var prod = new ProductsDTOResponse.Producto();
                    var loc = new ProductsDTOResponse.Localizacion();
                    var cac = new ProductsDTOResponse.Caracteristicas();
                    var cal = 0;
                    prod.IdProducto = item.IdProducto;
                    prod.CostoProducto = item.CostoProducto;
                    prod.NombreProducto = item.NombreProducto;
                    prod.Descripcion = item.Descripcion;
                    prod.EstadoProductoServicio = item.IdEstadoProductoServicio;
                    cac = await GetInfoCaracteristicas(item.IdCaracterizacionProducto);
                    loc = await GetInfoLocalizacion(item.IdLocalizacion);
                    cal = await GetInfoCalificacion(item.IdProducto);
                    string stars = "";
                    if (cal == 0)
                    {
                        stars = "Sin calificaciones.";
                    }
                    else
                    {
                        for (int i = 0; i < cal; i++)
                        {
                            stars += "★ ";
                        }
                    }

                    prod.EstrellasCalificacion = stars;
                    prod.NombreTipoProducto = await contextDB.TipoProducto.Where(x => x.IdTipoProducto == item.IdTipoProducto).Select(x => x.NombreTiipoProducto).FirstOrDefaultAsync();
                    prod.Localizacion = loc;
                    prod.Caracteristicas = cac;
                    prod.Calificacion= cal;
                    prod.Unidades = item.UnidadesProducto;
                    prod.Imagen = item.Imagen;
                    productInfo.producto = prod;
                    productsList.Add(productInfo);
                }

                result.Value = productsList;
                result.ContentType = "application/json";
                result.StatusCode = 200;

                return result;

            }
            catch (Exception ex)
            {
                var err = ex.Message;
                result.Value = err;
            }
            result.ContentType = "application/json";
            result.StatusCode = 400;

            return result;
        }

        public async Task<ProductsDTOResponse.Caracteristicas> GetInfoCaracteristicas(int idCaracterizacion)
        {
            var caracterizacionInfo = new ProductsDTOResponse.Caracteristicas();
            try
            {
                    var caracterizacion = await contextDB.CaracterizacionProducto.Where(x => x.IdCaracterizacionProducto == idCaracterizacion).FirstOrDefaultAsync();
                    caracterizacionInfo.Garantia = await contextDB.TipoGarantiaProducto.Where(x => x.IdTipoGarantiaProducto == caracterizacion.IdTipoGarantiaProducto).Select(x => x.Nombre).FirstOrDefaultAsync();
                    caracterizacionInfo.Material = await contextDB.TipoMaterialProducto.Where(x => x.IdTipoMaterialProducto == caracterizacion.IdTipoMaterialProducto).Select(x => x.Nombre).FirstOrDefaultAsync();
                    caracterizacionInfo.Ancho = caracterizacion.Ancho;
                    caracterizacionInfo.Alto = caracterizacion.Alto;
                    caracterizacionInfo.Condicion = caracterizacion.Condicion;
                    caracterizacionInfo.Modelo = caracterizacion.Modelo;
                    caracterizacionInfo.Marca = caracterizacion.Marca;
                    caracterizacionInfo.EnvioGratis = caracterizacion.EnvioGratis;
            }
            catch (Exception ex)
            {
                var ere = ex.Message;
                throw;
            }

            return caracterizacionInfo;
        }

        public async Task<ProductsDTOResponse.Localizacion> GetInfoLocalizacion(int idLocalization)
        {
            var LocalizacionInfo = new ProductsDTOResponse.Localizacion();
            try
            {
                    var localization = await contextDB.Localizacion.Where(x => x.IdLocalizacion == idLocalization).FirstOrDefaultAsync();
                    var infoCity = await contextDB.Municipio.Where(x => x.IdMunicipio == localization.IdMunicipio).FirstOrDefaultAsync();
                    LocalizacionInfo.Departmento = await contextDB.Departamento.Where(x => x.IdDepartamento == infoCity.IdDepartamento).Select(x => x.Nombre).FirstOrDefaultAsync();
                    LocalizacionInfo.Ciudad = infoCity.Nombre;
                    LocalizacionInfo.Barrio = await contextDB.Barrio.Where(x => x.IdBarrio == localization.IdBarrio).Select(x => x.Nombre).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                var ere = ex.Message;
                throw;
            }

            return LocalizacionInfo;
        }


        public async Task<int> GetInfoCalificacion(int idProducto)
        {
            var CalificacionInfo = 0;
            try
            {
                var calificaciones = await contextDB.CalificacionProducto.Where(x => x.IdProducto == idProducto).ToListAsync();
                if (calificaciones.Count == 0)
                {
                    CalificacionInfo = 0;

                    return CalificacionInfo;
                }
                var total = calificaciones.Sum(x => x.Calificacion);
                var totalCalificaciones = calificaciones.Count();
                var promedioCalificion = total / totalCalificaciones;
                CalificacionInfo = promedioCalificion;
        
            }
            catch (Exception ex)
            {
                var ere = ex.Message;
                throw;
            }

            return CalificacionInfo;
        }

        [HttpPost("qualifyProduct")]
        public async Task<JsonResult> QualifyProduct([FromBody] ProductsDTOQualify productData)
        {
            JsonResult result = new JsonResult(productData);
            try
            {
                if (productData.IdProducto == 0)
                {
                    result.ContentType = "application/json";
                    result.StatusCode = 400;

                    return result;
                }
                var calificacion = new CalificacionProducto();
                var productInfo = await contextDB.Producto.Where(x => x.IdProducto == productData.IdProducto).FirstOrDefaultAsync();
                calificacion.IdProducto = productData.IdProducto;
                calificacion.FechaCreacion = DateTime.Now;
                calificacion.Calificacion = productData.Calificacion;
                calificacion.Observacion = productData.Observacion;
                calificacion.IdUsuarioCalificado = productInfo.UsuarioCreacion;
                calificacion.IdUsuarioCliente = productData.IdUsuario;

                contextDB.CalificacionProducto.Add(calificacion);
                var saveC = await contextDB.SaveChangesAsync();

                if (saveC != 0)
                {
                    result.ContentType = "application/json";
                    result.StatusCode = 200;

                    return result;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                result.Value = err;
            }
            result.ContentType = "application/json";
            result.StatusCode = 400;

            return result;
        }


        [HttpPost("buyProduct")]
        public async Task<JsonResult> BuyProduct([FromBody] ProductsDTOBuy productData)
        {
            JsonResult result = new JsonResult(productData);
            try
            {
                if (productData.producto.IdProducto == 0)
                {
                    result.ContentType = "application/json";
                    result.StatusCode = 400;

                    return result;
                }
               
                var idVentasProductos = await saveVentasProductos(productData.producto);
                productData.envio.IdVentasProductos = idVentasProductos;
                var idEnvio = await saveEnvioProducto(productData.envio, productData.producto.IdUsuarioComprador);
                if (idEnvio != 0)
                {
                    await UpdateUnitsProduct(productData.producto.IdProducto, productData.producto.UnidadesCompradas);
                    result.ContentType = "application/json";
                    result.StatusCode = 200;

                    return result;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                result.Value = err;
            }
            result.ContentType = "application/json";
            result.StatusCode = 400;

            return result;
        }

        public async Task<int> saveVentasProductos(ProductsDTOBuy.Producto productInfo)
        {
            var idVentasProductos = 0;
            try
            {
                var prodInfo = await contextDB.Producto.Where(x => x.IdProducto == productInfo.IdProducto).FirstOrDefaultAsync();
                var venta = new VentasProductos
                {
                  UnidadesCompradas = productInfo.UnidadesCompradas,
                  ValorPorUnidad = (float)prodInfo.CostoProducto,
                  ValorTotal = (float)prodInfo.CostoProducto * productInfo.UnidadesCompradas,
                  FechaCreacion = DateTime.Now,
                  FechaModificacion = null,
                  UsuarioModificacion = null,
                  UsuarioCreacion = prodInfo.UsuarioCreacion,
                  IdProducto = productInfo.IdProducto,
                  IdUsuarioComprador = productInfo.IdUsuarioComprador,
                  IdUsuarioVendedor = prodInfo.UsuarioCreacion,
                  IdTipoPago = productInfo.IdTipoPago,
                  IdEstadoVenta = 3
                };

                var saveL = contextDB.VentasProductos.Add(venta);
                await contextDB.SaveChangesAsync();
                idVentasProductos = venta.IdVentasProductos;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }

            return idVentasProductos;
        }

        public async Task<int> saveEnvioProducto(ProductsDTOBuy.Envio envioInfo, string idUser)
        {
            var idEnvio = 0;
            try
            {
                var envio = new Envios
                {
                  NombreCompradors = envioInfo.NombreCompradors,
                  Telefono = envioInfo.Telefono,
                  CorreoElectronico = envioInfo.CorreoElectronico,
                  DireccionEnvio = envioInfo.DireccionEnvio,
                  DatosAdicionales = envioInfo.DatosAdicionales,
                  FechaCreacion = DateTime.Now,
                  FechaModificacion = null,
                  UsuarioModificacion = null,
                  UsuarioCreacion = idUser,
                  IdVentasProductos = envioInfo.IdVentasProductos,
                  IdUsuarioComprador = idUser,
                  IdMunicipio = envioInfo.IdMunicipio,
                  IdBarrio = envioInfo.IdBarrio
                };

                var saveL = contextDB.Envios.Add(envio);
                await contextDB.SaveChangesAsync();
                idEnvio = envio.IdEnvios;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }

            return idEnvio;
        }

        [HttpPost("registerProduct")]
            public async Task<JsonResult> RegisterProduct([FromBody] ProductsDTO productData)
            {
                JsonResult result = new JsonResult(productData);

                try
                {
                if (productData.localizacion == null)
                {
                    result.ContentType = "application/json";
                    result.StatusCode = 400;

                    return result;
                }
                    var idUser = await contextDB.Users.FirstOrDefaultAsync();
                    var idLocalization = await saveLocalization(productData.localizacion);
                    var idCharacterizationProduct = await saveCharacterizationProduct(productData.caracterizacion);
                    var product = new Producto
                    {
                        Descripcion = productData.producto.Descripcion,
                        NombreProducto = productData.producto.NombreProducto,
                        FechaModificacion = null,
                        UsuarioModificacion = null,
                        UsuarioCreacion = productData.producto.IdUsuario,
                        FechaCreacion = DateTime.Now,
                        UnidadesProducto = productData.producto.Unidades,
                        CostoProducto = productData.producto.CostoProducto,
                        IdTipoProducto = productData.producto.IdTipoProducto,
                        IdCaracterizacionProducto = idCharacterizationProduct,
                        IdEstadoProductoServicio = 1,
                        IdLocalizacion = idLocalization,
                        Imagen = productData.producto.Imagen
                    };

                    contextDB.Producto.Add(product);
                    var saveProduct = await contextDB.SaveChangesAsync();
                    if (saveProduct == 1)
                    {
                        result.ContentType = "application/json";
                        result.StatusCode = 200;

                        return result;
                    }
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    result.Value = err;
                }
                result.ContentType = "application/json";
                result.StatusCode = 400;
                return result;
            }

            public async Task<int> saveCharacterizationProduct(ProductsDTO.Characterization CharacterizationInfo)
            {
                var idCharacterizationProduct = 0;
                try
                {
                    var Characterization = new CaracterizacionProducto
                    {
                        Marca = CharacterizationInfo.Marca,
                        Modelo = CharacterizationInfo.Modelo,
                        Condicion = CharacterizationInfo.Condicion,
                        Ancho = CharacterizationInfo.Ancho,
                        Alto= CharacterizationInfo.Alto,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = null,
                        UsuarioModificacion = null,
                        UsuarioCreacion = null,
                        EnvioGratis = CharacterizationInfo.EnvioGratis,
                        IdTipoMaterialProducto = CharacterizationInfo.IdTipoMaterialProducto,
                        IdTipoGarantiaProducto = CharacterizationInfo.IdTipoGarantiaProducto
                    };

                    var saveL = contextDB.CaracterizacionProducto.Add(Characterization);
                    await contextDB.SaveChangesAsync();
                    idCharacterizationProduct = Characterization.IdCaracterizacionProducto;
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    throw;
                }

                return idCharacterizationProduct;
            }
            public async Task<int> saveLocalization(ProductsDTO.Localization localizationInfo)
            {
                var idLocalization = 0;
                try
                {

                    var Localization = new Localizacion
                    {
                        Direccion = localizationInfo.Direccion,
                        CodigoPostal = localizationInfo.CodigoPostal,
                        DatosAdicionales = localizationInfo.DatosAdicionales,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = null,
                        UsuarioModificacion = null,
                        UsuarioCreacion = null,
                        IdMunicipio = localizationInfo.IdMunicipio,
                        IdBarrio = localizationInfo.IdBarrio,
                        Telefono = localizationInfo.Telefono,
                        TelefonoOpc = localizationInfo.TelefonOpc,
                        Email = localizationInfo.Email,
                        WebSite= localizationInfo.WebSite
                    };

                    var saveL = contextDB.Localizacion.Add(Localization);
                    await contextDB.SaveChangesAsync();
                    idLocalization = Localization.IdLocalizacion;
                }
                catch (Exception ex)
                {
                    var err = ex.Message;
                    throw;
                }

                return idLocalization;
            }

        public async Task<bool> UpdateUnitsProduct(int idProducto, double cantidad)
        {
            try
            {
                var UnidadesProducto = await contextDB.Producto.Where(x => x.IdProducto == idProducto).Select(x => x.UnidadesProducto).FirstOrDefaultAsync();
                var cantidadActual = UnidadesProducto - cantidad;

                var result = await contextDB.Producto.Where(b => b.IdProducto == idProducto).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        result.UnidadesProducto = cantidadActual;
                        contextDB.SaveChanges();

                    return true;
                    }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }
            return true;
        }

    }
    }


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
    public class ServicesController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly SEI_Context contextDB;
        public ServicesController(IConfiguration configuration, SEI_Context contextDB)
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
                JsonResult result = new JsonResult(departments);
                departments = await contextDB.Departamento.ToListAsync();
                if (departments.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;

                    return NoContent();
                }

                result.Value = departments;
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

        [HttpGet("getCities")]
        public async Task<ActionResult> GetCities(int idDepartamento)
        {
            try
            {
                var municipios = new List<Municipio>();
                JsonResult result = new JsonResult(municipios);
                municipios = await contextDB.Municipio.Where(x => x.IdDepartamento == idDepartamento).ToListAsync();
                if (municipios.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;

                    return NoContent();
                }
                var optionSelect = new Municipio();
                int maxId = municipios.Max(u => u.IdMunicipio);
                optionSelect.IdMunicipio = maxId + 1;
                optionSelect.Nombre = "Seleccione";
                optionSelect.CodigoDane = "1";
                municipios.Add(optionSelect);
                List<Municipio> SortedList = municipios.OrderByDescending(o => o.IdMunicipio).ToList();
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

        [HttpGet("getNighborhoods")]
        public async Task<ActionResult> GetNighborhoods(int idMunicipio)
        {
            try
            {
                var barrios = new List<Barrio>();
                JsonResult result = new JsonResult(barrios);
                barrios = await contextDB.Barrio.Where(x => x.IdMunicipio == idMunicipio).ToListAsync();
                if (barrios.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return NoContent();
                }
                var optionSelect = new Barrio();
                int maxId = barrios.Max(u => u.IdBarrio);
                optionSelect.IdBarrio = maxId + 1;
                optionSelect.Nombre = "Seleccione";
                optionSelect.CodigoDane = "1";
                barrios.Add(optionSelect);
                List<Barrio> SortedList = barrios.OrderByDescending(o => o.IdBarrio).ToList();
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

        [HttpGet("getCategoryTypeService")]
        public async Task<ActionResult> GetCategoryTypeService()
        {
            try
            {
                var tipoCategoriasServicio = new List<TipoCategoriaServicio>();
                JsonResult result = new JsonResult(tipoCategoriasServicio);
                tipoCategoriasServicio = await contextDB.TipoCategoriaServicio.ToListAsync();
                if (tipoCategoriasServicio.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return NoContent();
                }
                var optionSelect = new TipoCategoriaServicio();
                int maxId = tipoCategoriasServicio.Max(u => u.IdTipoCategoriaServicio);
                optionSelect.IdTipoCategoriaServicio = maxId + 1;
                optionSelect.Nombre = "Seleccione";
                tipoCategoriasServicio.Add(optionSelect);
                List<TipoCategoriaServicio> SortedList = tipoCategoriasServicio.OrderByDescending(o => o.IdTipoCategoriaServicio).ToList();
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


        [HttpGet("getMotivoFinalizacion")]
        public async Task<ActionResult> GetMotivoFinalizacion()
        {
            try
            {
                var motivoFinalizacionServicios = new List<MotivoFinalizacionServicio>();
                JsonResult result = new JsonResult(motivoFinalizacionServicios);
                motivoFinalizacionServicios = await contextDB.MotivoFinalizacionServicio.ToListAsync();
                if (motivoFinalizacionServicios.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return NoContent();
                }
                var optionSelect = new MotivoFinalizacionServicio();
                int maxId = motivoFinalizacionServicios.Max(u => u.IdMotivoFinalizacionServicio);
                optionSelect.IdMotivoFinalizacionServicio = maxId + 1;
                optionSelect.Nombre = "Seleccione";
                motivoFinalizacionServicios.Add(optionSelect);
                List<MotivoFinalizacionServicio> SortedList = motivoFinalizacionServicios.OrderByDescending(o => o.IdMotivoFinalizacionServicio).ToList();
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

        [HttpGet("getServiceCategory")]
        public async Task<ActionResult> GetServiceCategory(int idTipoCategoriaServicio)
        {
            try
            {
                var ServiceCategory = new List<CategoriaServicio>();
                JsonResult result = new JsonResult(ServiceCategory);
                ServiceCategory = await contextDB.CategoriaServicio.Where(x => x.IdTipoCategoriaServicio == idTipoCategoriaServicio).ToListAsync();
                if (ServiceCategory.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return NoContent();
                }
                var optionSelect = new CategoriaServicio();
                int maxId = ServiceCategory.Max(u => u.IdCategoriaServicio);
                optionSelect.IdCategoriaServicio = maxId + 1;
                optionSelect.Nombre = "Seleccione";
                ServiceCategory.Add(optionSelect);
                List<CategoriaServicio> SortedList = ServiceCategory.OrderByDescending(o => o.IdCategoriaServicio).ToList();
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


        [HttpGet("getServiceType")]
        public async Task<ActionResult> GetServiceType(int idCategoriaServicio)
        {
            try
            {
                var Servicetype = new List<TipoServicio>();
                JsonResult result = new JsonResult(Servicetype);
                Servicetype = await contextDB.TipoServicio.Where(x => x.IdCategoriaServicio == idCategoriaServicio).ToListAsync();
                if (Servicetype.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;

                    return NoContent();
                }
                var optionSelect = new TipoServicio();
                int maxId = Servicetype.Max(u => u.IdTipoServicio);
                optionSelect.IdTipoServicio = maxId + 1;
                optionSelect.Nombre = "Seleccione";
                Servicetype.Add(optionSelect);
                List<TipoServicio> SortedList = Servicetype.OrderByDescending(o => o.IdTipoServicio).ToList();
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


        [HttpGet("getMessagesBuysById")]
        public async Task<JsonResult> GetMessagesBuysById(string idUser)
        {
            JsonResult result = new JsonResult(idUser);
            try
            {
                var mensajes = new List<MensajesVentaServicio>();
                var ventaProductos = new List<VentasProductos>();
                var mensajesServ = new List<SalesDTO.Mensajes>();

                mensajes = await contextDB.MensajesVentaServicio.Where(x => x.UsuarioComprador == idUser && x.IdMensajeRespuesta == null).ToListAsync();
                if (mensajes.Count != 0)
                {
                    foreach (var mensaje in mensajes)
                    {
                        var userInfo = await contextDB.Users.Where(x => x.Id == mensaje.UsuarioVendedor).FirstOrDefaultAsync();
                        var ventaS = await contextDB.VentasServicios.Where(x => x.IdVentasServicios == mensaje.IdVentasServicios).FirstOrDefaultAsync();
                        var msg = new SalesDTO.Mensajes
                        {
                            IdMensaje = mensaje.IdMensajesVentaServicio,
                            Mensaje = mensaje.Mensaje,
                            Respuesta = await contextDB.MensajesVentaServicio.Where(x => x.IdMensajeRespuesta == mensaje.IdMensajesVentaServicio).Select(x => x.Mensaje).FirstOrDefaultAsync(),
                            Servicio = await contextDB.Servicio.Where(x => x.IdServicio == ventaS.IdServicio).Select(x => x.NombreServicio).FirstOrDefaultAsync(),
                            Fecha = mensaje.FechaCreacion.ToString(),
                            NombreVendedor = userInfo.Name + " " + userInfo.Lastname
                        };
                        mensajesServ.Add(msg);
                    }
                }

                result.Value = mensajesServ;
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

        [HttpGet("getMessagesSalesById")]
        public async Task<JsonResult> GetMessagesSalesById(string idUser)
        {
            JsonResult result = new JsonResult(idUser);
            try
            {
                var mensajes = new List<MensajesVentaServicio>();
                var ventaProductos = new List<VentasProductos>();
                var mensajesServ = new List<SalesDTO.Mensajes>();

                mensajes = await contextDB.MensajesVentaServicio.Where(x => x.UsuarioVendedor == idUser && x.IdMensajeRespuesta == null).ToListAsync();
                if (mensajes.Count != 0)
                {
                    foreach (var mensaje in mensajes)
                    {
                        var userInfo = await contextDB.Users.Where(x => x.Id == mensaje.UsuarioComprador).FirstOrDefaultAsync();
                        var ventaS = await contextDB.VentasServicios.Where(x => x.IdVentasServicios == mensaje.IdVentasServicios).FirstOrDefaultAsync();
                        var msg = new SalesDTO.Mensajes
                        {
                            IdMensaje = mensaje.IdMensajesVentaServicio,
                            Mensaje = mensaje.Mensaje,
                            Respuesta = await contextDB.MensajesVentaServicio.Where(x => x.IdMensajeRespuesta == mensaje.IdMensajesVentaServicio).Select(x => x.Mensaje).FirstOrDefaultAsync(),
                            Servicio = await contextDB.Servicio.Where(x => x.IdServicio == ventaS.IdServicio).Select(x => x.NombreServicio).FirstOrDefaultAsync(),
                            Fecha = mensaje.FechaCreacion.ToString(),
                            NombreVendedor = userInfo.Name + " " + userInfo.Lastname
                        };
                        mensajesServ.Add(msg);
                    }
                }

                result.Value = mensajesServ;
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


        [HttpGet("getServicesAndProductsById")]
        public async Task<JsonResult> GetServicesAndProductsById(string idUser)
        {
            string strSearch = string.Empty;
            JsonResult result = new JsonResult(strSearch);
            try
            {
                var ventaServicios = new List<VentasServicios>();
                var ventaProductos = new List<VentasProductos>();
                var ventasPS = new List<SalesDTO.Ventas>();

                ventaServicios = await contextDB.VentasServicios.Where(x => x.IdUsuarioVendedor == idUser).ToListAsync();
                ventaProductos = await contextDB.VentasProductos.Where(x => x.IdUsuarioVendedor == idUser).ToListAsync();
                if (ventaServicios.Count != 0)
                {
                    foreach (var service in ventaServicios)
                    {
                        var userInfo = await contextDB.Users.Where(x => x.Id == service.IdUsuarioComprador).FirstOrDefaultAsync();
                        var ventaS = new SalesDTO.Ventas();
                        ventaS.Nombre = await contextDB.Servicio.Where(x => x.IdServicio == service.IdServicio).Select(x => x.NombreServicio).FirstOrDefaultAsync();
                        ventaS.Precio = service.ValoTotal;
                        ventaS.FechaVenta = service.FechaCreacion.ToString();
                        ventaS.NombreComprador = userInfo.Name + " " + userInfo.Lastname;
                        ventaS.Tipo = "Servicio";
                        ventaS.EstadoVenta = await contextDB.EstadoVenta.Where(x => x.IdEstadoVenta == service.IdEstadoVenta).Select(x => x.Nombre).FirstOrDefaultAsync();

                        ventasPS.Add(ventaS);
                    }
                }

                if (ventaProductos.Count != 0)
                {
                    foreach (var product in ventaProductos)
                    {
                        var userInfo = await contextDB.Users.Where(x => x.Id == product.IdUsuarioComprador).FirstOrDefaultAsync();
                        var ventaP = new SalesDTO.Ventas();
                        ventaP.Nombre = await contextDB.Producto.Where(x => x.IdProducto == product.IdProducto).Select(x => x.NombreProducto).FirstOrDefaultAsync();
                        ventaP.Precio = product.ValorTotal;
                        ventaP.FechaVenta = product.FechaCreacion.ToString();
                        ventaP.NombreComprador = userInfo.Name + " " + userInfo.Lastname;
                        ventaP.Tipo = "Producto";
                        ventaP.EstadoVenta = await contextDB.EstadoVenta.Where(x => x.IdEstadoVenta == product.IdEstadoVenta).Select(x => x.Nombre).FirstOrDefaultAsync();
                        
                        ventasPS.Add(ventaP);
                    }

                }

                result.Value = ventasPS;
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

        [HttpGet("updateStatusService")]
        public async Task<JsonResult> UpdateStatusService(int idServicio, int idEstado)
        {
            string strSearch = string.Empty;
            JsonResult result = new JsonResult(strSearch);
            try
            {
                var servicio = await contextDB.Servicio.Where(x => x.IdServicio == idServicio).FirstOrDefaultAsync();
                if (idEstado== 3)
                {
                    servicio.IdEstadoProductoServicio = 3;
                    contextDB.SaveChanges();

                    result.Value = "Registro Actualizado Correctamente";
                    result.ContentType = "application/json";
                    result.StatusCode = 200;

                    return result;
                }
                if (servicio.IdEstadoProductoServicio == 1)
                {
                    servicio.IdEstadoProductoServicio = 2;
                    contextDB.SaveChanges();
                }
                else
                {
                    servicio.IdEstadoProductoServicio = 1;
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

        [HttpGet("getServicesPostedByUser")]
        public async Task<JsonResult> GetServicesPostedByUser(string idUser)
        {
            string strSearch = string.Empty;
            JsonResult result = new JsonResult(strSearch);
            try
            {
                var services = new List<Servicio>();
                var servicesList = new List<ServicesDTOResponse>();
                services = await contextDB.Servicio.Where(x=>x.UsuarioCreacion == idUser && x.IdEstadoProductoServicio != 3).ToListAsync();
                foreach (var item in services)
                {
                    var serviceInfo = new ServicesDTOResponse();
                    var serv = new ServicesDTOResponse.Service();
                    var loc = new ServicesDTOResponse.Localizacion();
                    var cac = new ServicesDTOResponse.Caracteristicas();
                    int cal = 0;
                    serv.IdServicio = item.IdServicio;
                    serv.CostoServicio = item.CostoServicio;
                    serv.NombreServicio = item.NombreServicio;
                    serv.Descripcion = item.Descripcion;
                    serv.IdEstadoProductoServicio = item.IdEstadoProductoServicio;
                    cac = await GetInfoCaracteristicas(item.IdCaracterizacionServicio);
                    loc = await GetInfoLocalizacion(item.IdLocalizacion);
                    cal = await GetInfoCalificacion(item.IdServicio);
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
                    serv.AplicaConvenio = item.AplicaConvenio == true ? "Si" : "No";
                    serv.Calificacion = stars;
                    serv.TipoServicio = await contextDB.TipoServicio.Where(x => x.IdTipoServicio == item.IdTipoServicio).Select(x => x.Nombre).FirstOrDefaultAsync();
                    serv.localizacion = loc;
                    serv.Caracteristicas = cac;
                    serv.Imagen = item.Imagen;
                    serv.FechaPublicacion = item.FechaCreacion.ToString();
                    serv.EstadoPublicacion = await contextDB.EstadoProductoServicio.Where(x => x.IdEstadoProductoServicio == item.IdEstadoProductoServicio).Select(x => x.Nombre).FirstOrDefaultAsync();
                    serviceInfo.servicio = serv;
                    servicesList.Add(serviceInfo);
                }

                result.Value = servicesList;
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

        [HttpGet("getServices")]
        public async Task<JsonResult> GetServices()
        {
            string strSearch = string.Empty;
            JsonResult result = new JsonResult(strSearch);
            try
            {
                var services = new List<Servicio>();
                var servicesList = new List<ServicesDTOResponse>();
                var idUser = await contextDB.Users.FirstOrDefaultAsync();
                services = await contextDB.Servicio.Where(x=>x.IdEstadoProductoServicio == 1).ToListAsync();
                foreach (var item in services)
                {
                    var serviceInfo = new ServicesDTOResponse();
                    var serv = new ServicesDTOResponse.Service();
                    var loc = new ServicesDTOResponse.Localizacion();
                    var cac = new ServicesDTOResponse.Caracteristicas();
                    int cal = 0;
                    serv.IdServicio = item.IdServicio;
                    serv.CostoServicio = item.CostoServicio;
                    serv.NombreServicio = item.NombreServicio;
                    serv.Descripcion = item.Descripcion;
                    serv.IdEstadoProductoServicio = item.IdEstadoProductoServicio;
                    cac = await GetInfoCaracteristicas(item.IdCaracterizacionServicio);
                    loc = await GetInfoLocalizacion(item.IdLocalizacion);
                    cal = await GetInfoCalificacion(item.IdServicio);
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
                    serv.AplicaConvenio = item.AplicaConvenio == true ? "Si" : "No";
                    serv.Calificacion = stars;
                    serv.TipoServicio = await contextDB.TipoServicio.Where(x => x.IdTipoServicio == item.IdTipoServicio).Select(x => x.Nombre).FirstOrDefaultAsync();
                    serv.localizacion = loc;
                    serv.Caracteristicas = cac;
                    serv.Imagen = item.Imagen;
                    serviceInfo.servicio = serv;
                    servicesList.Add(serviceInfo);
                }

                result.Value = servicesList;
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

        public async Task<int> GetInfoCalificacion(int idServicio)
        {
            var CalificacionInfo = 0;
            try
            {
                var calificaciones = await contextDB.CalificacionServicio.Where(x => x.IdServicio == idServicio).ToListAsync();
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


        public async Task<ServicesDTOResponse.Caracteristicas> GetInfoCaracteristicas(int idCaracterizacion)
        {
            var caracterizacionInfo = new ServicesDTOResponse.Caracteristicas();
            try
            {
                var caracterizacion = await contextDB.CaracterizacionServicio.Where(x => x.IdCaracterizacionServicio == idCaracterizacion).FirstOrDefaultAsync();
                caracterizacionInfo.Incluye = caracterizacion.Incluye;
                caracterizacionInfo.NoIncluye = caracterizacion.NoIncluye;
                var infoUser = await contextDB.Users.Where(x => x.Id == caracterizacion.UsuarioCreacion).FirstOrDefaultAsync();
                var fullName = infoUser.Name + " " + infoUser.Lastname;
                caracterizacionInfo.Nombre = fullName;
                caracterizacionInfo.Telefono = infoUser.Phone;
                string exp = caracterizacion.Experiencia + " años";
                caracterizacionInfo.Experiencia = exp;
                caracterizacionInfo.Banco = infoUser.Bank;
                caracterizacionInfo.NumeroCuenta = infoUser.NumberAccount;

            }
            catch (Exception ex)
            {
                var ere = ex.Message;
                throw;
            }

            return caracterizacionInfo;
        }

        public async Task<ServicesDTOResponse.Localizacion> GetInfoLocalizacion(int idLocalization)
        {
            var LocalizacionInfo = new ServicesDTOResponse.Localizacion();
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

        [HttpPost("hireService")]
        public async Task<JsonResult> HireService([FromBody] ServicesDTOHire serviceData)
        {
            JsonResult result = new JsonResult(serviceData);
            try
            {
                if (serviceData.IdServicio == 0)
                {
                    result.ContentType = "application/json";
                    result.StatusCode = 400;

                    return result;
                }
                var serviceInfo = await contextDB.Servicio.Where(x => x.IdServicio == serviceData.IdServicio).FirstOrDefaultAsync();
                var idTipoPago = serviceInfo.CostoServicio == 0 ? 1 : 3;
                var hireService = new VentasServicios
                {
                    ValoTotal = serviceInfo.CostoServicio,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = null,
                    UsuarioModificacion = null,
                    UsuarioCreacion = serviceInfo.UsuarioCreacion,
                    IdServicio = serviceData.IdServicio,
                    IdUsuarioComprador = serviceData.IdUsuario,
                    IdUsuarioVendedor = serviceInfo.UsuarioCreacion,
                    IdTipoPago = idTipoPago,
                    IdEstadoVenta = 3,
                    IdMotivoFinalizacionServicio = 3,
                };

                contextDB.VentasServicios.Add(hireService);
                var saveService = await contextDB.SaveChangesAsync();
                if (saveService == 1)
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
        [HttpPost("finishService")]
        public async Task<JsonResult> FinishService([FromBody] ServicesDTOFinish serviceData)
        {
            JsonResult result = new JsonResult(serviceData);
            try
            {
                if (serviceData.IdServicio == 0)
                {
                    result.ContentType = "application/json";
                    result.StatusCode = 400;

                    return result;
                }
                var serviceInfo = await contextDB.VentasServicios.Where(x => x.IdVentasServicios == serviceData.IdVentaServicio).FirstOrDefaultAsync();
                serviceInfo.IdEstadoVenta = 2;
                serviceInfo.IdMotivoFinalizacionServicio = serviceData.IdMotivoFinalizacion;

                var updateService = await contextDB.SaveChangesAsync();
                if (updateService == 1)
                {
                    await QualifyService(serviceData);
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

        public async Task<JsonResult> QualifyService(ServicesDTOFinish serviceData)
        {
            JsonResult result = new JsonResult(serviceData);
            try
            {
                if (serviceData.IdServicio == 0)
                {
                    result.ContentType = "application/json";
                    result.StatusCode = 400;

                    return result;
                }
                var calificacion = new CalificacionServicio();
                var serviceInfo = await contextDB.Servicio.Where(x => x.IdServicio == serviceData.IdServicio).FirstOrDefaultAsync();
                calificacion.IdServicio = serviceData.IdServicio;
                calificacion.FechaCreacion = DateTime.Now;
                calificacion.Calificacion = serviceData.Calificacion;
                calificacion.Observacion = serviceData.Observacion;
                calificacion.IdUsuarioCalificado = serviceInfo.UsuarioCreacion;
                calificacion.IdUsuarioCliente = serviceData.IdUsuarioComprador;

                contextDB.CalificacionServicio.Add(calificacion);
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


        [HttpGet("getServicesByUser")]
        public async Task<ActionResult> GetServicesByUser(string idUser)
        {
            try
            {
                var listaServicios = new List<VentasServicios>();
                var productos = new List<ServicesDTOResponse>();
                JsonResult result = new JsonResult(listaServicios);
                listaServicios = await contextDB.VentasServicios.Where(x => x.IdUsuarioComprador == idUser).ToListAsync();
                if (listaServicios.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return NoContent();
                }
                foreach (var item in listaServicios)
                {
                    var servicioInfo = new ServicesDTOResponse();
                    var serv = new ServicesDTOResponse.Service();
                    var servicioC = await contextDB.Servicio.Where(x => x.IdServicio == item.IdServicio).FirstOrDefaultAsync();
                    var infoUser = await contextDB.Users.Where(x => x.Id == servicioC.UsuarioCreacion).FirstOrDefaultAsync();
                    var caracterizacion = await contextDB.CaracterizacionServicio.Where(x => x.IdCaracterizacionServicio == servicioC.IdCaracterizacionServicio).FirstOrDefaultAsync();
                    var adjuntos = await contextDB.AdjuntosVentasServicios.Where(x => x.IdVentasServicios == item.IdVentasServicios).Select(x => x.NombreDocumento).ToArrayAsync();
                    serv.Adjuntos = adjuntos;
                    serv.IdVentaServicio = item.IdVentasServicios;
                    serv.NombreServicio = servicioC.NombreServicio;
                    serv.CostoServicio = servicioC.CostoServicio;
                    serv.NombrePrestador = infoUser.Name + " " + infoUser.Lastname;
                    serv.Telefono = infoUser.Phone;
                    serv.Experiencia = caracterizacion.Experiencia + " Años";
                    serv.NombreBanco = infoUser.Bank;
                    serv.NumeroCuenta = infoUser.NumberAccount;
                    serv.TipoCuenta = infoUser.TypeAccount;
                    serv.EstadoVenta = await contextDB.EstadoVenta.Where(x => x.IdEstadoVenta == item.IdEstadoVenta).Select(x => x.Nombre).FirstOrDefaultAsync();
                    serv.MotivoFinalizacion = await contextDB.MotivoFinalizacionServicio.Where(x => x.IdMotivoFinalizacionServicio == item.IdMotivoFinalizacionServicio).Select(x => x.Nombre).FirstOrDefaultAsync();
                    serv.Imagen = servicioC.Imagen;
                    serv.IdServicio = servicioC.IdServicio;
                    servicioInfo.servicio = serv;

                    productos.Add(servicioInfo);
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

        [HttpPost("sendResponseMessageS")]
        public async Task<JsonResult> SendResponseMessageS([FromBody] ServicesDTOMessage message)
        {
            JsonResult result = new JsonResult(message);
            try
            {
                if (message.IdUsuario == "")
                {
                    result.ContentType = "application/json";
                    result.StatusCode = 400;

                    return result;
                }
                var idVentaS = await contextDB.MensajesVentaServicio.Where(x => x.IdMensajesVentaServicio == message.IdMensajesVentaServicio).Select(x => x.IdVentasServicios).FirstOrDefaultAsync();
                var ventaServicio = await contextDB.VentasServicios.Where(x => x.IdVentasServicios == idVentaS).FirstOrDefaultAsync();
                var messageInfo = new MensajesVentaServicio
                {
                    Mensaje = message.Mensaje,
                    FechaCreacion = DateTime.Now,
                    UsuarioComprador = ventaServicio.IdUsuarioComprador,
                    UsuarioVendedor = message.IdUsuario,
                    IdVentasServicios = idVentaS,
                    IdMensajeRespuesta = message.IdMensajesVentaServicio
                };
                contextDB.MensajesVentaServicio.Add(messageInfo);
                var saveMessage = await contextDB.SaveChangesAsync();
                if (saveMessage > 0)
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


        [HttpPost("sendMessage")]
        public async Task<JsonResult> SendMessage([FromBody] ServicesDTOMessage message)
        {
            JsonResult result = new JsonResult(message);
            try
            {
                if (message.IdVentaServicio == 0 || message.IdUsuario == "")
                {
                    result.ContentType = "application/json";
                    result.StatusCode = 400;

                    return result;
                }
                var ventaServicio = await contextDB.VentasServicios.Where(x => x.IdVentasServicios == message.IdVentaServicio).FirstOrDefaultAsync();
                var messageInfo = new MensajesVentaServicio
                {
                    Mensaje = message.Mensaje,
                    FechaCreacion = DateTime.Now,
                    UsuarioComprador = message.IdUsuario,
                    UsuarioVendedor = ventaServicio.IdUsuarioVendedor,
                    IdVentasServicios = message.IdVentaServicio,
                };
                contextDB.MensajesVentaServicio.Add(messageInfo);
                var saveMessage = await contextDB.SaveChangesAsync();
                if (saveMessage > 0)
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


        [HttpPost("uploadAttachments")]
        public async Task<JsonResult> UploadAttachments([FromBody] ServicesDTOAttachments attachments)
        {
            JsonResult result = new JsonResult(attachments);
            try
            {
                if (attachments.IdVentaServicio == 0 || attachments.url == "")
                {
                    result.ContentType = "application/json";
                    result.StatusCode = 400;

                    return result;
                }
                var attachmentsService = new AdjuntosVentasServicios();
                attachmentsService.UsuarioCreacion = attachments.IdUsuario;
                attachmentsService.IdVentasServicios = attachments.IdVentaServicio;
                attachmentsService.NombreDocumento = attachments.fileName;
                attachmentsService.Documento = attachments.url;
                attachmentsService.Activo = 1;
                attachmentsService.FechaCreacion = DateTime.Now;
                contextDB.AdjuntosVentasServicios.Add(attachmentsService);
                var saveService = await contextDB.SaveChangesAsync();
                if (saveService > 0)
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

        [HttpPost("registerService")]
        public async Task<JsonResult> RegisterService([FromBody] ServicesDTO serviceData)
        {
            JsonResult result = new JsonResult(serviceData);
            try
            {
                if (serviceData.localizacion == null)
                {
                    result.ContentType = "application/json";
                    result.StatusCode = 400;

                    return result;
                }
                var idLocalization = await saveLocalization(serviceData.localizacion);
                var idCharacterizationService = await saveCharacterizationService(serviceData.caracterizacion);
                bool aplicaConvenio = serviceData.servicio.AplicaConvenio == "Si" ? true : false;
                double costoServ = aplicaConvenio == true ? 0 : serviceData.servicio.CostoServicio;
                var service = new Servicio
                {
                    Descripcion = serviceData.servicio.Descripcion,
                    NombreServicio = serviceData.servicio.NombreServicio,
                    FechaModificacion = null,
                    UsuarioModificacion = null,
                    UsuarioCreacion = serviceData.servicio.IdUsuario,
                    FechaCreacion = DateTime.Now,
                    AplicaConvenio = aplicaConvenio,
                    CostoServicio = costoServ,
                    IdTipoServicio = serviceData.servicio.IdTipoServicio,
                    IdCaracterizacionServicio = idCharacterizationService,
                    IdEstadoProductoServicio = 1,
                    IdLocalizacion = idLocalization,
                    Imagen = serviceData.servicio.Imagen
                };

                contextDB.Servicio.Add(service);
                var saveService = await contextDB.SaveChangesAsync();
                if (saveService == 1)
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

        public async Task<int> saveCharacterizationService(ServicesDTO.Characterization CharacterizationInfo)
        {
            var idCharacterizationService = 0;
            try
            {
                var idUser = await contextDB.Users.FirstOrDefaultAsync();
                var Characterization = new CaracterizacionServicio
                {
                    Experiencia = CharacterizationInfo.Experiencia,
                    Incluye = CharacterizationInfo.Incluye,
                    NoIncluye = CharacterizationInfo.NoIncluye,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = null,
                    UsuarioModificacion = null,
                    UsuarioCreacion = idUser.Id
                };

                var saveL = contextDB.CaracterizacionServicio.Add(Characterization);
                await contextDB.SaveChangesAsync();
                idCharacterizationService = Characterization.IdCaracterizacionServicio;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }

            return idCharacterizationService;
        }
        public async Task<int> saveLocalization(ServicesDTO.Localization localizationInfo)
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
                    WebSite = localizationInfo.WebSite,
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

    }
}

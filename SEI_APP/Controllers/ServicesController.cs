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

                result.Value = municipios;
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

                result.Value = barrios;
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

                result.Value = tipoCategoriasServicio;
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
                result.Value = ServiceCategory;
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
                result.Value = Servicetype;
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
                //var productsList = await contextDB.Producto.Where(x => x.NombreProducto.Contains(strSearch)).ToListAsync();
                services = await contextDB.Servicio.ToListAsync();
                foreach (var item in services)
                {
                    var serviceInfo = new ServicesDTOResponse();
                    var serv = new ServicesDTOResponse.Service();
                    var loc = new ServicesDTOResponse.Localizacion();
                    var cac = new ServicesDTOResponse.Caracteristicas();
                    serv.IdServicio = item.IdServicio;
                    serv.CostoServicio = item.CostoServicio;
                    serv.NombreServicio = item.NombreServicio;
                    serv.Descripcion = item.Descripcion;
                    serv.IdEstadoProductoServicio = item.IdEstadoProductoServicio;
                    cac = await GetInfoCaracteristicas(item.IdCaracterizacionServicio);
                    loc = await GetInfoLocalizacion(item.IdLocalizacion);
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
                caracterizacionInfo.Calificacion = "★ ★ ★ ★";
                caracterizacionInfo.Experiencia = exp;
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
            if (serviceData.IdServicio == 0)
            {
                result.ContentType = "application/json";
                result.StatusCode = 400;

                return result;
            }
            
            try
            {
                var serviceInfo = await contextDB.Servicio.Where(x => x.IdServicio == serviceData.IdServicio).FirstOrDefaultAsync();
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
                    IdTipoPago = serviceData.IdTipoPago,
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
                    serv.NombreServicio = servicioC.NombreServicio;
                    serv.CostoServicio = servicioC.CostoServicio;
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

        [HttpPost("registerService")]
        public async Task<JsonResult> RegisterService([FromBody] ServicesDTO serviceData)
        {
            JsonResult result = new JsonResult(serviceData);
            if (serviceData.localizacion == null)
            {
                result.ContentType = "application/json";
                result.StatusCode = 400;

                return result;
            }

            try
            {
                var idUser = await contextDB.Users.FirstOrDefaultAsync();
                var idLocalization = await saveLocalization(serviceData.localizacion);
                var idCharacterizationService = await saveCharacterizationService(serviceData.caracterizacion);
                var service = new Servicio
                {
                    Descripcion = serviceData.servicio.Descripcion,
                    NombreServicio = serviceData.servicio.NombreServicio,
                    FechaModificacion = null,
                    UsuarioModificacion = null,
                    UsuarioCreacion = serviceData.servicio.IdUsuario,
                    FechaCreacion = DateTime.Now,
                    CostoServicio = serviceData.servicio.CostoServicio,
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
                    IdBarrio = localizationInfo.IdBarrio

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

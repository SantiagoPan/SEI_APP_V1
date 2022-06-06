﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SEI_APP.Areas.Identity.Data;
using SEI_APP.DTOs;
using SEI_APP.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SEI_APP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SEI_Context contextDB;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        public UsersController(UserManager<ApplicationUser> userManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager, SEI_Context contextDB)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.contextDB = contextDB;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ResponseAuthDto>> RegisterUser([FromBody] RegisterDto userData)
        {
            ResponseAuthDto response = new ResponseAuthDto();
            var bDate = Convert.ToDateTime(userData.Birthdate);
            var user = new ApplicationUser { UserName = userData.Email,
                                            Email = userData.Email, 
                                            EmailConfirmed = true,
                                            Name = userData.FirstName,
                                            Lastname = userData.Lastname,
                                            DocumentType = userData.DocumentType,
                                            Document = userData.Document,
                                            Phone = userData.Phone,
                                            Address = userData.Address,
                                            Birthdate = bDate,
                                            Active = true
            };
            var result = await userManager.CreateAsync(user, userData.Password);
            if (result.Succeeded)
            {
                var tokenData = new UserDto
                {
                    Email = userData.Email
                };
                return await BuildToken(tokenData, response);
            }

            else
            {
                return BadRequest(result.Errors);
            }

        }

        [HttpGet("validateUserRP")]
        public async Task<ActionResult<ResponseAuthDto>> ValidateUserRP(string TipoDocumento, string Documento, string Email)
        {
            JsonResult resultLogin = new JsonResult(Documento);
            var user = await contextDB.Users.Where(x => x.Email == Email && x.Document == Documento && x.DocumentType == TipoDocumento).FirstOrDefaultAsync();
            if (user == null)
            {
                resultLogin.Value = "NoExiste";
                resultLogin.ContentType = "application/json";
                resultLogin.StatusCode = 200;

                return resultLogin;
            }
            else
            {
                resultLogin.Value = "Existe";
                resultLogin.ContentType = "application/json";
                resultLogin.StatusCode = 200;

                return resultLogin;
            }
        }


        [HttpPost("sendNotification")]
        public async Task<ActionResult<ResponseAuthDto>> SendNotification(NotificationDTO Mensaje)
        {
            JsonResult changePass = new JsonResult(Mensaje);
            try
            {
                if (!string.IsNullOrWhiteSpace(Mensaje.Mensaje))
                {
                    var notificacion = new NotificacionMasiva();
                    notificacion.Mensaje = Mensaje.Mensaje;
                    notificacion.FechaMensaje = DateTime.Now;

                    contextDB.NotificacionMasiva.Add(notificacion);
                    var saveC = await contextDB.SaveChangesAsync();
                    changePass.Value = "Exitoso";
                    changePass.ContentType = "application/json";
                    changePass.StatusCode = 200;

                    return changePass;
                    
                    
                }
                changePass.Value = "Error";
                changePass.ContentType = "application/json";
                changePass.StatusCode = 200;

                return changePass;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }

        }

        [HttpGet("getNotificationAdmin")]
        public async Task<ActionResult> GetNotificationAdmin()
        {
            try
            {
                var notifications = new List<NotificacionMasiva>();
                JsonResult result = new JsonResult(notifications);
                notifications = await contextDB.NotificacionMasiva.ToListAsync();

                if (notifications == null)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    
                    return NoContent();
                }

                result.Value = notifications;
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


        [HttpPost("login")]
        public async Task<ActionResult<ResponseAuthDto>> Login([FromBody] UserDto userData)
        {
            JsonResult resultLogin = new JsonResult(userData);
            try
            {
                var result = await signInManager.PasswordSignInAsync(userData.Email, userData.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    ResponseAuthDto response = new ResponseAuthDto();
                    var infoUser = await contextDB.Users.Where(x => x.Email == userData.Email && x.Active == true).FirstOrDefaultAsync();
                    if (infoUser == null)
                    {
                        resultLogin.Value = "Usuario Inactivo";
                        resultLogin.ContentType = "application/json";
                        resultLogin.StatusCode = 200;

                        return resultLogin;
                    }
                    response.IdUser = infoUser.Id;
                    response.IsAdmin = infoUser.IsAdmin;
                    var respToken = await BuildToken(userData, response);
                    resultLogin.Value = respToken;
                    resultLogin.ContentType = "application/json";
                    resultLogin.StatusCode = 200;

                    return resultLogin;
                }
                else
                {
                    resultLogin.Value = "LoginError";
                    resultLogin.ContentType = "application/json";
                    resultLogin.StatusCode = 200;

                    return resultLogin;
                }
            }
            catch (Exception ex)
            {
                var ms = ex.Message;
                throw;
            }

        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult<ResponseAuthDto>> ResetPassword(string UserName, string Password)
        {
            JsonResult changePass = new JsonResult(UserName);
            try
            {
                if (!string.IsNullOrWhiteSpace(UserName))
                {
                    var returnUser = await userManager.Users.Where(x => x.UserName == UserName).FirstOrDefaultAsync();

                    var code = await userManager.GeneratePasswordResetTokenAsync(returnUser);

                    if (returnUser != null)
                    {
                        var reset = await userManager.ResetPasswordAsync(returnUser, code, Password);
                        if (reset.Succeeded)
                        {
                            changePass.Value = "Exitoso";
                            changePass.ContentType = "application/json";
                            changePass.StatusCode = 200;

                            return changePass;
                        }
                        else
                        {
                            changePass.Value = "NoExitoso";
                            changePass.ContentType = "application/json";
                            changePass.StatusCode = 200;

                            return changePass;
                        }
                    }
                }
                changePass.Value = "LoginError";
                changePass.ContentType = "application/json";
                changePass.StatusCode = 200;

                return changePass;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }

        }


        [HttpPost("saveBankInfo")]
        public async Task<ActionResult<ResponseAuthDto>> SaveBankInfo([FromBody] RegisterDto.InfoBank dataBank)
        {
            JsonResult result = new JsonResult(dataBank);
            try
            {
                if (dataBank.IdUser == "")
                {
                    result.Value = "IdUser no encontrado";
                    result.ContentType = "application/json";
                    result.StatusCode = 400;

                    return result;
                }
                var user = await contextDB.Users.Where(x=>x.Id == dataBank.IdUser).FirstOrDefaultAsync();

                user.Bank = dataBank.Bank;
                user.TypeAccount = dataBank.TypeAccount;
                user.NumberAccount = dataBank.NumberAccount;

                var updateBankInfo = await contextDB.SaveChangesAsync();
                if (updateBankInfo != 0)
                {
                    result.Value = "Información Bancaria Actuaizada.";
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

        [HttpGet("getInfoBank")]
        public async Task<ActionResult> GetInfoBank(string idUser)
        {
            try
            {
                var infoBank = new List<RegisterDto.InfoBank>();
                JsonResult result = new JsonResult(infoBank);
                var userInfo = await contextDB.Users.Where(x => x.Id == idUser).FirstOrDefaultAsync();

                if (userInfo == null)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return NoContent();
                }
                var bank = new RegisterDto.InfoBank();
                bank.Id = 1;
                bank.Bank = userInfo.Bank;
                bank.NumberAccount = userInfo.NumberAccount;
                bank.TypeAccount = userInfo.TypeAccount;

                infoBank.Add(bank);
                result.Value = infoBank;
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
        [HttpGet("updateStateUser")]
        public async Task<JsonResult> UpdateStateUser(string idUsuario)
        {
            string strSearch = string.Empty;
            JsonResult result = new JsonResult(strSearch);
            try
            {
                var user = await contextDB.Users.Where(x => x.Id == idUsuario).FirstOrDefaultAsync();
                if (user.Active)
                {
                    user.Active = false;
                    contextDB.SaveChanges();
                }
                else
                {
                    user.Active = true;
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

        [HttpGet("getUsers")]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var usersList = new List<RegisterDto.UpdateInfoUser>();
                JsonResult result = new JsonResult(usersList);
                var users = await contextDB.Users.ToListAsync();

                if (users == null)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return NoContent();
                }
                foreach (var userInfo in users)
                {
                    var infoUser = new RegisterDto.UpdateInfoUser.userInfo();
                    var user = new RegisterDto.UpdateInfoUser();

                    infoUser.IdUser = userInfo.Id;
                    infoUser.State = userInfo.Active == true ? "Activo" : "Inactivo";
                    infoUser.FirstName = userInfo.Name;
                    infoUser.Lastname = userInfo.Lastname;
                    infoUser.Address = userInfo.Address;
                    infoUser.Birthdate = userInfo.Birthdate.ToString("yyyy-MM-dd");
                    infoUser.Document = userInfo.Document;
                    infoUser.Email = userInfo.Email;
                    infoUser.Phone = userInfo.Phone;
                    infoUser.DocumentType = userInfo.DocumentType;
                    user.user = infoUser;
                    usersList.Add(user);
                }

                result.Value = usersList;
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


        [HttpGet("getInfoUser")]
        public async Task<ActionResult> GetInfoUser(string idUser)
        {
            try
            {
                var infoUser = new RegisterDto();
                JsonResult result = new JsonResult(infoUser);
                var userInfo = await contextDB.Users.Where(x => x.Id == idUser).FirstOrDefaultAsync();

                if (userInfo == null)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return NoContent();
                }
                infoUser.IdUser = userInfo.Id;
                infoUser.FirstName = userInfo.Name;
                infoUser.Lastname = userInfo.Lastname;
                infoUser.Address = userInfo.Address;
                infoUser.Birthdate = userInfo.Birthdate.ToString("yyyy-MM-dd");
                infoUser.Document = userInfo.Document;
                infoUser.Email = userInfo.Email;
                infoUser.Phone = userInfo.Phone;
                infoUser.DocumentType = userInfo.DocumentType;
                
                result.Value = infoUser;
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

        [HttpPost("updateInfoUser")]
        public async Task<ActionResult> UpdateInfoUser([FromBody] RegisterDto.UpdateInfoUser.userInfo infoUser)
        {
            try
            {
                var user = new RegisterDto.UpdateInfoUser.userInfo();
                JsonResult result = new JsonResult(infoUser);
                var userInfo = await contextDB.Users.Where(x => x.Id == infoUser.IdUser).FirstOrDefaultAsync();

                if (userInfo == null)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;
                    return NoContent();
                }
                userInfo.Name = infoUser.FirstName;
                userInfo.Lastname = infoUser.Lastname;
                userInfo.Address = infoUser.Address;
                userInfo.Birthdate = Convert.ToDateTime(infoUser.Birthdate);
                userInfo.Document = infoUser.Document;
                userInfo.Email = infoUser.Email;
                userInfo.Phone = infoUser.Phone;
                userInfo.DocumentType = infoUser.DocumentType;
                contextDB.SaveChanges();

                result.Value = infoUser;
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

        // crear el token
        private async Task<ResponseAuthDto> BuildToken(UserDto register, ResponseAuthDto response)
        {
            var claims = new List<Claim>(){
                new Claim("email", register.Email)
            };
            var usuario = await userManager.FindByEmailAsync(register.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);
            var clavs = "AHRAOPOKGPAJERGIPAENRGNERFNERPVANPOELPHSOA";
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clavs));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var exp = DateTime.UtcNow.AddDays(1);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
            expires: exp, signingCredentials: creds);

            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.Expiration = exp;

            return response;

        }

        [HttpGet("getDocumentType")]
        public async Task<ActionResult> GetDocumentType()
        {
            try
            {
                var documentTypes = new List<TipoDocumentoIdentidad>();
                JsonResult result = new JsonResult(documentTypes);
                documentTypes = await contextDB.TipoDocumentoIdentidad.ToListAsync();
                if (documentTypes.Count == 0)
                {
                    result.Value = null;
                    result.ContentType = "application/json";
                    result.StatusCode = 200;

                    return NoContent();
                }

                result.Value = documentTypes;
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

    }
}

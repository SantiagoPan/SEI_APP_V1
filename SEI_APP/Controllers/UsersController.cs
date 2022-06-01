
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
                                            Birthdate = bDate

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

        [HttpPost("login")]
        public async Task<ActionResult<ResponseAuthDto>> Login([FromBody] UserDto userData)
        {
            JsonResult resultLogin = new JsonResult(userData);
            var result = await signInManager.PasswordSignInAsync(userData.Email, userData.Password, isPersistent: false, lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                ResponseAuthDto response = new ResponseAuthDto();
                var infoUser = await contextDB.Users.Where(x=>x.Email == userData.Email).FirstOrDefaultAsync();
                response.IdUser = infoUser.Id;
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
                infoUser.FirstName = userInfo.Name;
                infoUser.Lastname = userInfo.Lastname;
                infoUser.Address = userInfo.Address;
                infoUser.Birthdate = userInfo.Birthdate.ToString();
                infoUser.Document = userInfo.Document;
                infoUser.Email = userInfo.Email;
                infoUser.Phone = userInfo.Phone;
                userInfo.DocumentType = userInfo.DocumentType;
                
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

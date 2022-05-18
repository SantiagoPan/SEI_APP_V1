
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

            var result = await signInManager.PasswordSignInAsync(userData.Email, userData.Password, isPersistent: false, lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                ResponseAuthDto response = new ResponseAuthDto();
                var infoUser = await contextDB.Users.Where(x=>x.Email == userData.Email).FirstOrDefaultAsync();
                response.IdUser = infoUser.Id;

                return await BuildToken(userData, response);
            }
            else
            {
                return BadRequest("Login Incorrecto!!");
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
                var erd = ex.Message;
                throw;
            }
        }

    }
}

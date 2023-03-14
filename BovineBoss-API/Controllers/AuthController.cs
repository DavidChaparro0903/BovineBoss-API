using Azure.Core;
using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BovineBoss_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsConfig")]
    //Controlador de autenticación y autorización
    public class AuthController : ControllerBase
    {
        private readonly IAdminService _personaService;
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration _configuration, IAdminService personaService)
        {
            //Configuración para acceso a archivo de propiedades 'appsettings.json'
            this.configuration = _configuration;
            _personaService = personaService;
        }
        /// <author>
        /// Diego Ballesteros
        /// </author>
        /// <summary>
        /// Metodo de autenticación de usuario y generación de Token
        /// </summary>
        /// <returns>
        /// Retorna una token con claims idUsuario y Rol
        /// </returns>
        /// <response code="200">Si la solicitud se ejecutó con éxito</response>
        /// <response code="400">Si la solicitud no se ejecuta correctamente</response>
        /// <response code="401">Si el usuario no está autenticado</response>
        /// <response code="403">Si el usuario no tiene permisos para realizar la solicitud</response>
        /// <response code="500">Si ocurre un error en el servidor</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login(String usuario, String contrasenia)
        {
            Response r = new Response();
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasenia))
            {
                r.errors = "Campos Usuario y contraseña no deben ir vacios";
                return BadRequest(r);
            }
            var queryResult = await _personaService.GetUser(usuario);
            if (queryResult == null)
            {
                r.errors = "Nombre de usuario o contraseña incorrectos";
                return Unauthorized(r);
            }
            LoginPersonaDTO user = queryResult;
            if (!BCrypt.Net.BCrypt.Verify(contrasenia, user.Contrasenia))
            {
                r.errors = "Nombre de usuario o contraseña incorrectos";
                return Unauthorized(r);
            }
            string token = JWTTokenGenerator(user);
            r.data = token;
            r.message = "token";
            return Ok(r);
        }

        //private string CreateJWT(LoginPersonaDTO persona)
        //{
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, persona.IdPersona.ToString()),
        //        new Claim(ClaimTypes.Role, persona.RolPersona)
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Key").Value!));

        //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var token = new JwtSecurityToken(claims:claims, expires:DateTime.Now.AddHours(6), signingCredentials:credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        private string JWTTokenGenerator(LoginPersonaDTO auth)
        {
            //cabecera
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var _signingCredentials = new SigningCredentials(
                _symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var _Header = new JwtHeader(_signingCredentials);
            //claims
            var _Claims = new[]
            {
                new Claim("Role", auth.RolPersona),
                new Claim("CredentialId", auth.IdPersona + "")
            };
            //payload
            var _Payload = new JwtPayload(
                    issuer: configuration["JWT:JWTIssuer"],
                    audience: configuration["JWT:JWTAudience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddHours(6));
            //token
            return new JwtSecurityTokenHandler().WriteToken(
                new JwtSecurityToken(_Header, _Payload));
        }
    }
}

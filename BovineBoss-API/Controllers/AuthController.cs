using Azure.Core;
using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
using BovineBoss_API.Services.Implementacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BovineBoss_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Controlador de autenticación y autorización
    public class AuthController : ControllerBase
    {
        private readonly IPersonaService _personaService;
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration _configuration, IPersonaService personaService)
        {
            //Configuración para acceso a archivo de propiedades 'appsettings.json'
            this.configuration = _configuration;
            _personaService = personaService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Persona>> Register(Persona persona)
        {
            Debug.WriteLine("Petición de creación alcanzada");
            if (persona.TipoPersona.Equals("A") || persona.TipoPersona.Equals("T"))
            {
                try
                {
                    if (persona.Usuario.IsNullOrEmpty()) throw new Exception();
                    string hashContrasenia = BCrypt.Net.BCrypt.HashPassword(persona.Contrasenia);
                    persona.Contrasenia = hashContrasenia;
                    Console.WriteLine(hashContrasenia.Length);
                } catch(Exception e)
                {
                    return Ok("Contraseña y nombre de usuario requeridos para registro");
                }
            }
            await _personaService.AddPersona(persona);

            return Ok(persona);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(String usuario, String contrasenia)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasenia))
            {
                return BadRequest("Nombre de y contraseña usuario necesarios");
            }
            var queryResult = await _personaService.GetPersona(usuario);
            if (queryResult == null)
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos");
            }
            Persona p = queryResult;
            if (!BCrypt.Net.BCrypt.Verify(contrasenia, p.Contrasenia))
            {
                return Unauthorized("Nombre de usuario o contraseña incorrectos");
            }
            LoginPersonaDTO pdto = new LoginPersonaDTO(p.IdPersona, p.NombrePersona + " " + p.ApellidoPersona, p.Usuario, p.TipoPersona);
            string token = CreateJWT(pdto);
            return Ok(token);
        }

        private string CreateJWT(LoginPersonaDTO persona)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, persona.IdPersona.ToString()),
                new Claim(ClaimTypes.NameIdentifier, persona.NombreUsuario),
                new Claim(ClaimTypes.Name, persona.NombrePersona),
                new Claim(ClaimTypes.Role, persona.RolPersona)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Key").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims:claims, expires:DateTime.Now.AddHours(6), signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

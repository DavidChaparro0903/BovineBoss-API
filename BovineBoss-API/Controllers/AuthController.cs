using BovineBoss_API.Models.DB;
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
                    return BadRequest(error: "Contraseña y nombre de usuario requeridos para registro");
                }
            }
            await _personaService.AddPersona(persona);

            return Ok(persona);
        }
        [HttpGet]
        public async Task Test()
        {
            await _personaService.GetList();
        }
    }
}

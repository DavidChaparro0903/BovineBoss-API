using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BovineBoss_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsConfig")]
    [Authorize]
    public class TrabajadorController : ControllerBase
    {

        private ITrabajadorService trabajadorService;


        public TrabajadorController(ITrabajadorService personaService)
        {
            this.trabajadorService = personaService;
        }


        [HttpPost("registerTrabajador")]
        public async Task<IActionResult> addAdmin(CreateEmployeeDto trabajador)
        {
            Response r = new();
            ActiveTrabajadorDto trabajadorAgregate = await trabajadorService.ActiveTrabajador(trabajador);
            if (trabajadorAgregate != null)
            {
                r.message = "Agregado correctamente";
                r.data = trabajadorAgregate;
                return Ok(r);
            }
            r.errors = "No se pudo agregar el administrador";
            return BadRequest(r);
        }



    }
}

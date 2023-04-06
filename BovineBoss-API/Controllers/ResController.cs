using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BovineBoss_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsConfig")]
    [Authorize]
    public class ResController : Controller
    {
        private IResService resService;

        public ResController(IResService resService)
        {
            this.resService = resService;
        }
        [HttpPost("addRaza")]
        public async Task<IActionResult> AddRaza(RazaDTO nuevaRaza)
        {
            Response r = new();
            Raza newRaza = await resService.AddRaza(nuevaRaza);
            if(newRaza != null)
            {
                r.message = "Agregado Correctamente";
                r.data = newRaza;
                return Ok(r);
            }
            else
            {
                r.errors = "Hubo un problema agregando la raza a la base de datos";
                return BadRequest(r);
            }
        }
        [HttpPost("addInconveniente")]
        public async Task<IActionResult> AddInconveniente(IncovenienteDTO incovenienteDTO)
        {
            Response r = new();
            Inconveniente newIssue = await resService.AddInconveniente(incovenienteDTO);
            if (newIssue != null)
            {
                r.message = "Agregado Correctamente";
                r.data = newIssue;
                return Ok(r);
            }
            else
            {
                r.errors = "Hubo un problema agregando el inconveniente a la base de datos";
                return BadRequest(r);
            }
        }
    }
}

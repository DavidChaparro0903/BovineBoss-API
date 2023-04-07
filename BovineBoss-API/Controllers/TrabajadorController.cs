using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
using BovineBoss_API.Services.Implementacion;
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




        /*
         Se registra trabajador y se añade a una finca
         
         */

        [HttpPost("registerTrabajador")]
        public async Task<IActionResult> addTrabajador(CreateEmployeeDto trabajador)
        {
            Response r = new();
            ActiveTrabajadorDto trabajadorAgregate = await trabajadorService.ActiveTrabajador(trabajador);
            if (trabajadorAgregate != null)
            {
                r.message = "Agregado correctamente";
                r.data = trabajadorAgregate;
                return Ok(r);
            }
            r.errors = "No se pudo agregar el trabajador";
            return BadRequest(r);
        }

        [HttpPut("ActualizarTrabajador")]
        public async Task<IActionResult> updateTrabajador(ModifyTrabajadorDto trabajador)
        {
            Response r = new();
            bool var = await trabajadorService.UpdateTrabajador(trabajador);
            if (var)
            {
                r.message = "Modificado correctamente";
                r.data = var;
                return Ok(r);
            }
            r.errors = "No se pudo modificar";
            return BadRequest(r);
        }



        [HttpPost("AgregarTrabajadorFinca")]
        public async Task<IActionResult> addStateEmployee(CreateNewEstateDto createNewEstateDto)
        {
            Response r = new();
            bool var = await trabajadorService.addNewEstate(createNewEstateDto);
            if (var)
            {
                r.message = "El trabajador fue agregado a las fincas correspondientes";
                r.data = var;
                return Ok(r);
            }
            r.errors = "Las fincas no existen o el trabajador ya esta en alguna de las fincas seleccionadas";
            return BadRequest(r);
        }



        [HttpPut("ActualizarTrabajadorAdmin")]
        public async Task<IActionResult> updateTrabajadorAdmin(ModifyTrabajadorAdminDto trabajador)
        {
            Response r = new();
            bool var = await trabajadorService.UpdateTrabajador(trabajador);
            if (var)
            {
                r.message = "Modificado correctamente";
                r.data = var;
                return Ok(r);
            }
            r.errors = "No se pudo modificar";
            return BadRequest(r);
        }


        





    }
}

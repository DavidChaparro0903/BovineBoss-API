using BovineBoss_API.Services.Contrato;
using Microsoft.AspNetCore.Mvc;
using BovineBoss_API.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using BovineBoss_API.Services.Implementacion;
using BovineBoss_API.Models.DB;

namespace BovineBoss_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsConfig")]
    [Authorize]
    public class AdminController : ControllerBase
    {


        private IAdminService personaService;


        public AdminController(IAdminService personaService)
        {
            this.personaService = personaService;
        }

        [HttpGet]
        public async Task<IActionResult> getListAdmin()
        {
            Response r = new();
            List<EmployeeDto> adminList = await personaService.GetListAdmin();
            if (adminList.Count > 0)
            {
                r.message = "Se obtiene exitosamente los administradores";
                r.data = adminList;
                return Ok(r);
            }
            r.errors = "No se puede listar fincas";
            return BadRequest(r);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getAdmin(int id)
        {
            Response r = new();
            EmployeeDto adminDto = await personaService.GetEmployeeDto(id);
            if (adminDto != null) 
            {
                r.message = "Se obtiene correctamente";
                r.data = adminDto;
                return Ok(r);
            }
            r.errors = "No se pudo obtener administrador";
            return BadRequest(r);
        }

        [AllowAnonymous]
        [HttpPost ("registerAdmin")]
        public async Task<IActionResult> addAdmin(CreateEmployeeDto Admin)
        {
            Response r = new();
            ActiveAdminDto adminAgregate = await personaService.ActiveAdmin(Admin);
            if (adminAgregate != null)
            {
                r.message = "Agregado correctamente";
                r.data = adminAgregate;
                return Ok(r);
            }
            r.errors = "No se pudo agregar el administrador";
            return BadRequest(r);
        }


        [HttpPut("ActualizarTrabajador")]
        public async Task<IActionResult> updateTrabajador(ModifyTrabajadorAdminDto trabajador)
        {
            Response r = new();
            bool var = await personaService.UpdateTrabajador(trabajador);
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


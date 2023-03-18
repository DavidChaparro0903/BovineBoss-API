using BovineBoss_API.Services.Contrato;
using Microsoft.AspNetCore.Mvc;
using BovineBoss_API.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

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
        public async Task<Response> getListAdmin()
        {
            return new Response() 
            {
                data = await personaService.GetListAdmin(),
                errors ="",
                message="Lista de usuario",
            };
        }

        [HttpGet("{id}")]
        public async Task<AdminDto> getAdmin(int id)
        {
            return await personaService.GetPersona(id);
        }

        [AllowAnonymous]
        [HttpPost ("registerAdmin")]
        public async Task<CreateEmployeeDto> addAdmin(CreateEmployeeDto Admin)
        {
            return await personaService.AddAdministrator(Admin);
        }



    }
}

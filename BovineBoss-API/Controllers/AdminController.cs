using BovineBoss_API.Services.Contrato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {


        private IAdminService personaService;


        public AdminController(IAdminService personaService)
        {
            this.personaService = personaService;
        }

        [HttpGet]
        public async Task<List<AdminDto>> getListAdmin()
        {
            return await personaService.GetListAdmin();
        }

        [HttpGet("{id}")]
        public async Task<AdminDto> getAdmin(int id)
        {
            return await personaService.GetPersona(id);
        }


        [HttpPost ("registerAdmin")]
        public async Task<CreateEmployeeDto> addAdmin(CreateEmployeeDto Admin)
        {
            return await personaService.AddAdministrator(Admin);
        }



    }
}

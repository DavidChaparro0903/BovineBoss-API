using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BovineBoss_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateController : ControllerBase
    {


        private IFincaService fincaService;
        public EstateController(IFincaService fincaService) {
            this.fincaService = fincaService;
        }

        [HttpGet]
        public async Task<List<FincaDto>> GetList()
        {
            return await fincaService.GetList();
        }


        [HttpGet("{id}")]
       public async Task<FincaDto> GetFinca(int id)
        {
            return await fincaService.GetFinca(id);
        }

        [HttpPost]
        public async Task<FincaDto> AddFinca(FincaDto fincaDto)
        {
            return await fincaService.AddFinca(fincaDto);

        }


    }
}

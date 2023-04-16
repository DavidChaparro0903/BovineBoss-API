using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
using BovineBoss_API.Services.Implementacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BovineBoss_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsConfig")]
    [ApiController]
    [Authorize]
    public class EstateController : ControllerBase
    {


        private IFincaService fincaService;
        public EstateController(IFincaService fincaService) {
            this.fincaService = fincaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            Response r = new();
            List<FincaDto> fincaList = await fincaService.GetList();
            if (fincaList.Count > 0)
            {
                r.message = "Se obtiene exitosamente las fincas";
                r.data = fincaList;
                return Ok(r);
            }
            r.errors = "No se puede listar fincas";
            return BadRequest(r);
        }


        [HttpGet("{id}")]
       public async Task<IActionResult> GetFinca(int id)
        {
            Response r = new();
            FincaDto fincaAgregate = await fincaService.GetFinca(id);
            if (fincaAgregate != null) 
            {
                r.message = "Se obtiene correctamente";
                r.data = fincaAgregate;
                return Ok(r);
            }
            r.errors = "No se puede obtener la finca especificada";
            return BadRequest(r);
        }

        [HttpPost]
        public async Task<IActionResult> AddFinca(CreateFincaDto fincaDto)
        {
            Response r = new();
            CreateFincaDto fincaAgregate = await fincaService.AddFinca(fincaDto);
            if (fincaAgregate != null)
            {
                r.message = "Agregado correctamente";
                r.data = fincaAgregate;
                return Ok(r);
            }
            r.errors = "No se puede agregar una finca";
            return BadRequest(r);
        }


      

        [HttpPut("ActualizarFinca")]
        public async Task<IActionResult> updateFinca(Finca finca)
        {
            Response r = new();
            bool var = await fincaService.UpdateFinca(finca);
            if (var)
            {
                r.message = "Modificado correctamente";
                r.data = var;
                return Ok(r);
            }
            r.errors = "No se pudo modificar";
            return BadRequest(r);
        }
        [HttpGet("GetUserEstates")]
        public async Task<IActionResult> getUserEstates([FromQuery] int userId)
        {
            Response r = new();
            try
            {
                r.data = await fincaService.GetListStateByIdUser(userId);
                return Ok(r);
            } catch 
            {
                r.errors = "No se encontraron fincas al usuario";
                return BadRequest(r);
            }
        }


    }
}

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
    public class CostController : ControllerBase
    {

        private ICostService costService;
        public CostController(ICostService costService)
        {
            this.costService = costService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            Response r = new();
            List<CostCompleteDto> costList = await costService.GetListCost();
            if (costList.Count > 0)
            {
                r.message = "Se obtiene exitosamente la lista de costos";
                r.data = costList;
                return Ok(r);
            }
            r.errors = "No se puede listar los costos";
            return BadRequest(r);
        }

        [HttpPost("AddCost")]

        public async Task<IActionResult> AddCost(CostDto cost)
        {
            Response r = new();
            bool isCostFood = await costService.AddCost(cost);
            if (isCostFood)
            {
                r.message = "Agregado Correctamente el costo";
                r.data = isCostFood;
                return Ok(r);
            }
            else
            {
                r.errors = "Hubo un problema agregando el costo";
                return BadRequest(r);
            }
        }

        [HttpPut("UpdateCost")]

        public async Task<IActionResult> UpdateCost(ModifyCost modifyCost)
        {
            Response r = new();
            bool var = await costService.ModifyCost(modifyCost);
            if (var)
            {
                r.message = "Modificado correctamente el costo";
                r.data = var;
                return Ok(r);
            }
            r.errors = "No se pudo modificar los costos";
            return BadRequest(r);
        }


        [HttpPost("AddCostToState")]

        public async Task<IActionResult> AddCostToState(CostStateDto costStateDto)
        {
            Response r = new();
            bool var = await costService.AddCostToState(costStateDto);
            if (var)
            {
                r.message = "Se agregado correctamente un gasto a la finca";
                r.data = var;
                return Ok(r);
            }
            r.errors = "No se pudo agregar el gasto";
            return BadRequest(r);
        }





        [HttpPut("ModifyCostToState")]

        public async Task<IActionResult> ModifyCostToState(ModifyCostStateDto costStateDto)
        {
            Response r = new();
            bool var = await costService.ModifyCostToState(costStateDto);
            if (var)
            {
                r.message = "Se ha modificado correctamente un gasto de la finca";
                r.data = var;
                return Ok(r);
            }
            r.errors = "No se pudo agregar el gasto";
            return BadRequest(r);
        }

        [HttpGet("GetCostsByState")]
        public async Task<IActionResult> GetCostsByState(int idFinca)
        {
            Response r = new();
            try
            {
                List<CostByStateDTO> result = await costService.GetCostsByState(idFinca);
                if (result.Count > 0)
                {
                    r.message = "Listado de costos por finca";
                    r.data = result;
                    return Ok(r);
                }
                else
                {
                    r.message = "La finca no tiene gastos asociados";
                    return Ok(r);
                }
            }catch (Exception ex)
            {
                r.errors = ex.ToString();
                return BadRequest(r);
            }
        }
    }
}

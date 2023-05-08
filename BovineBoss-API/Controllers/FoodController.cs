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
    [EnableCors("CorsConfig")]
    [ApiController]
    [Authorize]
    public class FoodController : ControllerBase
    {



        private IFoodService foodService;


        public FoodController(IFoodService foodService)
        {
            this.foodService = foodService;
        }


        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            Response r = new();
            List<FoodDto> foodList = await foodService.GetListFood();
            if (foodList.Count > 0)
            {
                r.message = "Se obtiene exitosamente la lista de alimentos";
                r.data = foodList;
                return Ok(r);
            }
            r.errors = "No se puede listar alimentos";
            return BadRequest(r);
        }


        [HttpPost("registerFood")]

        public async Task<IActionResult> AddFood(CreateFoodDto food)
        {
            Response r = new();
            bool isAddFood = await foodService.AddFood(food);
            if (isAddFood)
            {
                r.message = "Agregado Correctamente";
                r.data = isAddFood;
                return Ok(r);
            }
            else
            {
                r.errors = "Hubo un problema agregando el alimento";
                return BadRequest(r);
            }
        }



        [HttpPost("AddFoodState")]

        public async Task<IActionResult> AddFoodState(FoodStateDto foodStateDto)
        {
            Response r = new();
            bool isAddFood = await foodService.AddFoodToState(foodStateDto);
            if (isAddFood)
            {
                r.message = "Agregado Correctamente";
                r.data = isAddFood;
                return Ok(r);
            }
            else
            {
                r.errors = "Hubo un problema agregando el alimento a la finca";
                return BadRequest(r);
            }
        }


        [HttpPut("UpdateFood")]
        public async Task<IActionResult> UpdateFood(FoodDto foodDto)
        {
            Response r = new();
            bool var = await foodService.ModifyFood(foodDto);
            if (var)
            {
                r.message = "Modificado correctamente";
                r.data = var;
                return Ok(r);
            }
            r.errors = "No se pudo modificar alimentos";
            return BadRequest(r);
        }
        [HttpGet("FoodByState")]
        public async Task<IActionResult> ListFoodByState(int idFinca)
        {
            Response r = new();
            try
            {
                var result = await foodService.GetFoodByEstate(idFinca);

                r.message = "Listado de Alimentos vinculados a una finca";
                r.data = result;
                return Ok(r);
            }catch
            {
                r.errors = "Hubo un problema accediendo al listado de alimentos";
                return BadRequest(r);
            }
        }

        [HttpPost("RegisterConsumption")]
        public async Task<IActionResult> AddConsumo(ConsumoDTO consumo)
        {
            Response r = new();
            String result = await foodService.AddFoodConsumo(consumo);
            if (result.Equals("Agregado"))
            {
                r.message = "Consumo agregado exitosamente";
                r.data = result;
                return Ok(r);
            }else if(result.Equals("Cantidad Invalida"))
            {
                r.errors = "La cantidad ingresada supera a la presente";
                return BadRequest(r);
            }else if (result.Equals("NoCows"))
            {
                r.errors = "La finca ingresada no cuenta con reses";
                return BadRequest(r);
            }
            else {
                r.errors ="Error agregando a BD";
                return BadRequest(r);
            }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetFood(int id)
        {
            Response r = new();
            FoodDto foodList = await foodService.GetFood(id);
            if (foodList != null)
            {
                r.message = "Se obtuvo el alimento correctamente";
                r.data = foodList;
                return Ok(r);
            }
            r.errors = "No se puede obtener el alimento";
            return BadRequest(r);
        }




        [HttpPut("ModifyFincaAlimento")]
        public async Task<IActionResult> ModifyFincaAlimento(FoodStateDto foodStateDto)
        {
            Response r = new();
            bool food = await foodService.ModifyFoodState(foodStateDto);
            if (food)
            {
                r.message = "Se modifico correctamente la finca alimento";
                r.data = food;
                return Ok(r);
            }
            r.errors = "No se pudo modificar correctamente la finca alimento";
            return BadRequest(r);
        }




    }
}

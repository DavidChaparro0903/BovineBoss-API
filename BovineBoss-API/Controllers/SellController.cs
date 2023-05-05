using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
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
    public class SellController : ControllerBase
    {
        private ISellService sellService;

        public SellController(ISellService sellService)
        {
            this.sellService = sellService;
        }

        [HttpPost("registerSale")]
        public async Task<IActionResult> registerSale(CreateSellDto sell)
        {
            Response r = new();
            CreateSellDto sellAgregate = await sellService.registerSell(sell);
            if(sellAgregate != null)
            {
                r.message = "Lista completa de ventas con Reses relacionadas";
                r.data = sellAgregate;
                return Ok(r);
            }
            r.errors = "Hubo un problema agregando el registro a la base de datos";
            return BadRequest(r);
        }
        [HttpGet("getSales")]
        public async Task<IActionResult> getSales()
        {
            Response r = new();
            List<SellDTO> sales = await sellService.getListVentas();
            if (sales != null)
            {
                r.message = "Agregado Correctamente";
                r.data = sales;
                return Ok(r);
            }
            r.errors = "Hubo un problema obteniendo la lista de Ventas";
            return BadRequest(r);
        }

        [HttpPut("changeSale")]
        public async Task<IActionResult> modifySale(SellDTO sellDTO)
        {
            Response r = new();
            try
            {
                await sellService.ModifySell(sellDTO);
                r.message = "Modificado Correctamente";
                r.data = sellDTO;
                return Ok(r);
            }
            catch
            {
                r.errors = "Hubo un problema modificando el registro";
                return BadRequest(r);
            }
        }


        [HttpGet("GetListVentasFincas")]
        public async Task<IActionResult> ListVentasFincas(int idFinca)
        {
            Response r = new();
            List<VentasDto> listVentasDto = await sellService.GetListVentasFincas(idFinca);
            if (listVentasDto != null)
            {
                r.message = "Se lista correctamente las ventas de la finca";
                r.data = listVentasDto;
                return Ok(r);

            }
            r.errors = "Hubo un problema listando las ventas en esa finca";
            return BadRequest(r);



        }




    }
}

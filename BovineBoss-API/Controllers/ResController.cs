using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
using BovineBoss_API.Services.Implementacion;
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

        [HttpPut("modifyInconveniente")]
        public async Task<IActionResult> UpdateInconveniente(ModifyInconvenienteDto inconvenienteDto)
        {
            Response r = new();
            bool var = await resService.UpdateInconveniente(inconvenienteDto);
            if (var)
            {
                r.message = "Modificado correctamente";
                r.data = var;
                return Ok(r);
            }
            r.errors = "No se pudo modificar";
            return BadRequest(r);
        }

        [HttpPost("registerRes")]
        public async Task<IActionResult> AddRes(CreateResDto createResDto)
        {
            Response r = new();
            CreateResDto newRes = await resService.AddRes(createResDto);
            if(newRes != null)
            {
                r.message = "Agregado Correctamente";
                r.data = newRes;
                return Ok(r);
            }
            else
            {
                r.errors = "Hubo un problema agregando la res a la base de datos";
                return BadRequest(r);
            }
        }
        [HttpPut("actualizarRes")]
        public async Task<IActionResult> updateRes(ModifyResDTO updatedResDTO)
        {
            Response r = new();
            ModifyResDTO updatedRes = await resService.UpdateRes(updatedResDTO);
            if(updatedRes != null)
            {
                r.message = "Actualizada Correctamente";
                r.data = updatedRes;
                return Ok(r);
            }
            else
            {
                r.errors = "La res a actualizar no se ha encontrado en la Base de Datos";
                return BadRequest(r);
            }
        }
        [HttpGet("getRaza")]
        public async Task<IActionResult> GetRazas()=> Ok(new Response()
            {
                data = await resService.GetRazas(),
                message = "Listado de razas"
            });
        [HttpGet("getDrawBacks")]
        public async Task<IActionResult> GetDrawBacks() => Ok(new Response()
        {
            data = await resService.GetDrawBacks(),
            message = "Listado de razas"
        });
        [HttpGet("getBulls")]
        public async Task<IActionResult> GetBulls(int stateId)
        {
            try
            {
                var r = await resService.GetBulls(stateId);
                return Ok(new Response()
                {
                    data = r,
                    message = "Listado de reses"
                });
            } 
            catch 
            {
                return BadRequest(new Response()
                {
                    errors = "No se pudieron listar las reses"
                });
            }
            
        }
        [HttpGet("getFullBulls")]
        public async Task<IActionResult> GetfULLBulls(int stateId)
        {
            try
            {
                var r = await resService.GetFullBull(stateId);
                return Ok(new Response()
                {
                    data = r,
                    message = "Listado de reses"
                });
            }
            catch
            {
                return BadRequest(new Response()
                {
                    errors = "No se pudieron listar las reses"
                });
            }

        }
        [HttpGet("getBullInconveniets")]
        public async Task<IActionResult> GetBullInconvenients(int bullId)
        {
            try
            {
                var r = await resService.GetBullInconvenients(bullId);
                return Ok(new Response()
                {
                    data = r,
                    message = "Listado de reses"
                });
            }
            catch
            {
                return BadRequest(new Response()
                {
                    errors = "No se pudieron listar las reses"
                });
            }

        }


        [HttpPost("AddResInconvenientes")]

        public async Task<IActionResult> AddResInconvenientes(AddResInconvenientesDto addResInconvenientesDto)
        {
            try
            {
                bool isAdd = await resService.AddResInconvenientes(addResInconvenientesDto);
                if (isAdd)
                {
                    return Ok(new Response()
                    {
                        data = isAdd,
                        message = "Reses Agregado"

                    });

                }
               return BadRequest(new Response()
                {
                    errors = "No se pudieron añadir los inconvenientes a la res"
                });
              
            }
            catch
            {
                return BadRequest(new Response()
                {
                    errors = "No se pudieron añadir los inconvenientes a la res"
                });

            }
        }



    }
}

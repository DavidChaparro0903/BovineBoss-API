﻿using BovineBoss_API.Models.DB;
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
    }
}

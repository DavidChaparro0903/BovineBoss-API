﻿using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface IResService
    {
        Task<Raza> AddRaza(RazaDTO nuevaRaza);
        Task<Inconveniente> AddInconveniente(IncovenienteDTO incovenienteDTO);
        Task<CreateResDto> AddRes(CreateResDto createResDto);

        Task<bool> UpdateInconveniente(ModifyInconvenienteDto inconvenienteDto);
        Task<ModifyResDTO> UpdateRes(ModifyResDTO updatedResDTO);
        Task<IEnumerable<Raza>> GetRazas();
        Task<IEnumerable<Rese>> GetBulls(int stateId);
        Task<IEnumerable<ResInconveniente>> GetBullInconvenients(int bullId);

        Task<bool> AddResInconvenientes(AddResInconvenientesDto addResInconvenientesDto);

        Task<IEnumerable<FullBullDto>> GetFullBull(int stateId);
        Task<IEnumerable<Inconveniente>> GetDrawBacks();

        Task<IEnumerable<FullBullDto>> GetBullsNotSold(int stateId);
    }
}
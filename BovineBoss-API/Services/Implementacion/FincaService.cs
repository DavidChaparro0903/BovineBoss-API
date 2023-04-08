﻿using Microsoft.EntityFrameworkCore;
using BovineBoss_API.Models;
using BovineBoss_API.Services.Contrato;
using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Implementacion
{
    public class FincaService : IFincaService
    {
        private BovineBossContext dbContext;

        public FincaService(BovineBossContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<CreateFincaDto?> AddFinca(CreateFincaDto fincaDto)
        {

            try
            {
                Finca finca = new () { 
                NombreFinca = fincaDto.NombreFinca,
                DireccionFinca = fincaDto.DireccionFinca,
                ExtensionFinca = fincaDto.ExtensionFinca
                };
                dbContext.Fincas.Add(finca);
                await dbContext.SaveChangesAsync();
                return fincaDto;
            }
            catch {

                return null;

            }

        }

        public async Task<bool> DeleteFinca(Finca finca)
        {
            try { 
                dbContext.Fincas.Remove(finca);
                await dbContext.SaveChangesAsync();
                return true;
            }catch 
            {
                return false;
            }
        }

        public Task<bool> fincaExits(int idFinca)
        {
            throw new NotImplementedException();
        }

        public async Task<FincaDto?> GetFinca(int idFinca)
        {

            try
            {
                var finca = await dbContext.Fincas.Where(e => e.IdFinca == idFinca).FirstOrDefaultAsync();
                FincaDto fincaDto = new ()
                {
                    IdFinca = finca.IdFinca,
                    NombreFinca = finca.NombreFinca,
                    DireccionFinca = finca.DireccionFinca,
                    ExtensionFinca = finca.ExtensionFinca
                };
                return fincaDto;

            }
            catch 
            {
                return null;
            }
        }


        public async Task<List<FincaDto>> GetList()
        {
            try {
                var listaFinca = await dbContext.Fincas.ToListAsync();
                List<FincaDto> listaFincaDto = listaFinca.Select(
                  f => new FincaDto {
                      IdFinca = f.IdFinca,
                      NombreFinca = f.NombreFinca,
                      DireccionFinca = f.DireccionFinca,
                      ExtensionFinca = f.ExtensionFinca
                  }).ToList();
                return listaFincaDto;
            } catch 
            {
                return null;
            }

            }

        public async Task<List<StateTokenDto>> GetListState()
        {
            var listState = await dbContext.Fincas.ToListAsync();
            List<StateTokenDto> listResultState = listState.Select(f => new StateTokenDto { 
                IdFinca = f.IdFinca,
                NombreFinca = f.NombreFinca
            }).ToList();
            return listResultState;
        }

        public async Task<bool> UpdateFinca(Finca finca)
        {
            try
            {

                dbContext.Fincas.Update(finca);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;

            }
        }


    }
}

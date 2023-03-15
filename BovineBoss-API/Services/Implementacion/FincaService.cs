using Microsoft.EntityFrameworkCore;
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

        public async Task<FincaDto> AddFinca(FincaDto fincaDto)
        {

            try
            {
                Finca finca = new Finca() { 
                NombreFinca = fincaDto.NombreFinca,
                DireccionFinca = fincaDto.DireccionFinca,
                ExtensionFinca = fincaDto.ExtensionFinca
                };
                dbContext.Fincas.Add(finca);
                await dbContext.SaveChangesAsync();
                return fincaDto;
            }
            catch (Exception ex) {
                throw ex;

            }

        }

        public async Task<bool> DeleteFinca(Finca finca)
        {
            try { 
                dbContext.Fincas.Remove(finca);
                await dbContext.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FincaDto> GetFinca(int idFinca)
        {

            try
            {
                Finca? finca = new Finca();
                finca = await dbContext.Fincas.Where(e => e.IdFinca == idFinca).FirstOrDefaultAsync();
                FincaDto fincaDto = new FincaDto
                {
                    NombreFinca = finca.NombreFinca,
                    DireccionFinca = finca.DireccionFinca,
                    ExtensionFinca = finca.ExtensionFinca
                };
                return fincaDto;

            }
            catch (Exception ex)
            {
                throw ex;


            }
        }

        public async Task<List<FincaDto>> GetList()
        {
            try {
                List<Finca> listaFinca = new List<Finca>();
                listaFinca = await dbContext.Fincas.ToListAsync();
                List<FincaDto> listaFincaDto = listaFinca.Select(
                  f => new FincaDto {
                      NombreFinca = f.NombreFinca,
                      DireccionFinca = f.DireccionFinca,
                      ExtensionFinca = f.ExtensionFinca
                  }).ToList();
                return listaFincaDto;
            } catch (Exception e)
            {
                throw e;

            }

            }

        public async Task<bool> UpdateFinca(Finca finca)
        {
            try
            {
                dbContext.Fincas.Update(finca);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;

            }
        }


    }
}

using Microsoft.EntityFrameworkCore;
using BovineBoss_API.Models;
using BovineBoss_API.Services.Contrato;
using BovineBoss_API.Models.DB;

namespace BovineBoss_API.Services.Implementacion
{
    public class FincaService : IFincaService
    {
        private BovineBossContext dbContext;

        public FincaService(BovineBossContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Finca> AddFinca(Finca finca)
        {

            try
            {
                dbContext.Fincas.Add(finca);
                await dbContext.SaveChangesAsync();
                return finca;
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

        public async Task<Finca> GetFinca(int idFinca)
        {

            try
            {
                Finca? finca = new Finca();

                finca = await dbContext.Fincas.Where(e => e.IdFinca == idFinca).FirstOrDefaultAsync();
                return finca;

            }
            catch (Exception ex)
            {
                throw ex;


            }
        }

        public async Task<List<Finca>> GetList()
        {
            try {
                List<Finca> listaFinca = new List<Finca>();
                listaFinca = await dbContext.Fincas.ToListAsync();
                return listaFinca;
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

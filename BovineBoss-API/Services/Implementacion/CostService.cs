using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace BovineBoss_API.Services.Implementacion
{
    public class CostService : ICostService
    {


        private BovineBossContext dbContext;


        public CostService(BovineBossContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> AddCost(CostDto cost)
        {
            try
            {
                Gasto gasto = new()
                {
                    TipoGasto = cost.TipoGasto
                };
                dbContext.Gastos.Add(gasto);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;
            }
        }



        public async Task<List<CostCompleteDto>> GetListCost()
        {
            try
            {
                return await dbContext.Gastos.Select(a => new CostCompleteDto()
                {
                    IdGasto = a.IdGasto,
                    TipoGasto = a.TipoGasto
                }).ToListAsync();
            }
            catch
            {
                return null;

            }
        }

        public async Task<bool> ModifyCost(ModifyCost modify)
        {
            try
            {
                Gasto gasto = await dbContext.Gastos.Where(a => a.IdGasto == modify.IdGasto).FirstOrDefaultAsync();

                if (gasto != null)
                {
                    gasto.TipoGasto = modify.TipoGasto;
                    dbContext.Gastos.Update(gasto);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                return false;


            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> AddCostToState(CostStateDto costStateDto)
        {
            try
            {
                FincaGasto fincaGasto = new()
                {
                    IdFinca = costStateDto.IdFinca,
                    IdGasto = costStateDto.IdGasto,
                    ValorGasto = costStateDto.ValorGasto,
                    FechaGasto = DateTime.ParseExact(DateTime.UtcNow.ToString("MM-dd-yyyy HH:mm:ss"), "MM-dd-yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    DescripcionGasto = costStateDto.DescripcionGasto
                };
                dbContext.FincaGastos.Add(fincaGasto);
                await dbContext.SaveChangesAsync();
                return true;


            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> ModifyCostToState(ModifyCostStateDto costStateDto)
        {
            FincaGasto fincaGasto = await dbContext.FincaGastos.Where(a => a.IdGasto == costStateDto.IdGasto && a.IdFinca == costStateDto.IdFinca && a.FechaGasto == costStateDto.FechaGasto).FirstOrDefaultAsync();
            if (fincaGasto != null)
            {
                fincaGasto.DescripcionGasto = costStateDto.DescripcionGasto;
                fincaGasto.ValorGasto = costStateDto.ValorGasto;
                dbContext.FincaGastos.Update(fincaGasto);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

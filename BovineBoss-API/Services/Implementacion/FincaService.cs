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

        public async Task<List<StateTokenDto>> GetListStateByIdUser(int userId)
        {
            List<StateTokenDto> estates = new();
            var user = dbContext.Personas.Where(x=> x.IdPersona == userId).First();
            if (user.TipoPersona == "A")
            {
                var lista = dbContext.AdministradorFincas.Where(x => x.IdAdministrador == user.IdPersona).ToList();
                estates = lista.Select(x => new StateTokenDto { 
                IdFinca = x.IdFinca,
                NombreFinca = dbContext.Fincas.Where(y => y.IdFinca == x.IdFinca).First().NombreFinca
                }).ToList();
            } 
            else
            {
                var lista = dbContext.TrabajadorFincas.Where(x => x.IdTrabajador == user.IdPersona).ToList();
                estates = lista.Select(x => new StateTokenDto
                {
                    IdFinca = x.IdFinca,
                    NombreFinca = dbContext.Fincas.Where(y => y.IdFinca == x.IdFinca).First().NombreFinca
                }).ToList();
            }
            return estates;
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

        public async Task<CostsReportDTO> GetCostsReport(int idFinca)
        {
            var finca_gastos = await dbContext.FincaGastos.Where(fg => fg.IdFinca == idFinca).OrderBy(fg => fg.FechaGasto).ToListAsync();
            var finca_alimento = await dbContext.FincaAlimentos.Where(fa => fa.IdFinca == idFinca).OrderBy(fa => fa.FechaCompra).ToListAsync();
            var res_inconvenientes = await dbContext.ResInconvenientes
                .Join(dbContext.Reses,
                    resInconveniente => resInconveniente.IdRes,
                    res => res.IdRes,
                    (resInconveniente, res) => new { ResInconveniente = resInconveniente, Res = res })
                .Where(joinResult => joinResult.Res.IdFinca == idFinca)
                .Select(joinResult => joinResult.ResInconveniente)
                .OrderBy(ri => ri.FechaInconveniente)
                .ToListAsync();

            Dictionary<DateOnly, int> tempValues = new Dictionary<DateOnly, int>();
            CostsReportDTO result = new CostsReportDTO();
            DateOnly currentDate = DateOnly.FromDateTime(finca_gastos[0].FechaGasto);

            tempValues[currentDate] = 0;

            foreach (FincaGasto gasto in finca_gastos)
            {
                if (DateOnly.FromDateTime(gasto.FechaGasto) <= currentDate)
                {
                    tempValues[currentDate] += gasto.ValorGasto;
                }
                else
                {
                    currentDate = DateOnly.FromDateTime(gasto.FechaGasto);
                    tempValues[currentDate] = gasto.ValorGasto;
                }
            }
            currentDate = DateOnly.FromDateTime(finca_alimento[0].FechaCompra);

            foreach (FincaAlimento fa in finca_alimento)
            {
                if (!tempValues.ContainsKey(currentDate))
                    tempValues[currentDate] = 0;
                if (DateOnly.FromDateTime(fa.FechaCompra) <= currentDate)
                {
                    tempValues[currentDate] += fa.PrecioAlimento;
                }
                else
                {
                    currentDate = DateOnly.FromDateTime(fa.FechaCompra);
                    tempValues[currentDate] = fa.PrecioAlimento;
                }
            }
            currentDate = DateOnly.FromDateTime(res_inconvenientes[0].FechaInconveniente);

            foreach (ResInconveniente ri in res_inconvenientes)
            {
                if (!tempValues.ContainsKey(currentDate))
                    tempValues[currentDate] = 0;
                if(DateOnly.FromDateTime(ri.FechaInconveniente) <= currentDate)
                {
                    tempValues[currentDate] += ri.DineroGastado;
                }
                else
                {
                    currentDate = DateOnly.FromDateTime(ri.FechaInconveniente);
                    tempValues[currentDate] = ri.DineroGastado;
                }
            }

            result.fechas = new DateOnly[tempValues.Count];
            result.valores = new int[tempValues.Count];
            int index = 0;
            foreach (KeyValuePair<DateOnly, int> kvp in tempValues)
            {
                result.fechas[index] = kvp.Key;
                result.valores[index] = kvp.Value;
                index++;
            }
            return result;
        }

        public async Task<EarningsReportDTO> GetEarningsReport(int idFinca)
        {
            var ventas_reses = await dbContext.Ventas
                .Join(
                    dbContext.Reses.Where(res => res.IdFinca == idFinca && res.ValorVenta != null),
                    venta => venta.IdVenta,
                    res => res.IdVenta,
                    (venta, res) => new { Venta = venta, Res = res }
                 ).OrderBy(value=>value.Venta.FechaVenta)
                 .ToListAsync();
            SortedDictionary<DateOnly, int> tempValues = new SortedDictionary<DateOnly, int>();
            EarningsReportDTO result = new EarningsReportDTO();
            
            DateOnly currentDate = DateOnly.FromDateTime(ventas_reses[0].Venta.FechaVenta);
            tempValues[currentDate] = 0;

            foreach(var value in ventas_reses)
            {
                if (DateOnly.FromDateTime(value.Venta.FechaVenta) <= currentDate)
                {
                    tempValues[currentDate] += value.Res.ValorVenta ?? 0;
                }
                else
                {
                    currentDate = DateOnly.FromDateTime(value.Venta.FechaVenta);
                    tempValues[currentDate] = value.Res.ValorVenta ?? 0;
                }
            }
            result.fechas = new DateOnly[tempValues.Count];
            result.valores = new int[tempValues.Count];
            int index = 0;
            foreach (KeyValuePair<DateOnly, int> kvp in tempValues)
            {
                result.fechas[index] = kvp.Key;
                result.valores[index] = kvp.Value;
                index++;
            }
            return result;
        }
    }
}

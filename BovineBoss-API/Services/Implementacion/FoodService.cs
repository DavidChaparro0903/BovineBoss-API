using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;
using BovineBoss_API.Services.Contrato;
using Microsoft.EntityFrameworkCore;

namespace BovineBoss_API.Services.Implementacion
{
    public class FoodService : IFoodService
    {



        private BovineBossContext dbContext;

        public FoodService(BovineBossContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> AddFood(CreateFoodDto food)
        {


            try
            {
                Alimento alimento = new()
                {
                    TipoAlimento = food.TipoAlimento
                };
                dbContext.Alimentos.Add(alimento);
                await dbContext.SaveChangesAsync();
                return true;


            }
            catch
            {

                return false;
            }

        }

        public async Task<List<FoodDto>> GetListFood()
        {

            try
            {
                return await dbContext.Alimentos.Select(a => new FoodDto()
                {
                    IdAlimento = a.IdAlimento,
                    TipoAlimento = a.TipoAlimento
                }).ToListAsync();
            }
            catch
            {
                return null;

            }

        }
        public async Task<String> AddFoodConsumo(ConsumoDTO consumo)
        {
            //Recupera lista de reses relacionadas a la finca donde se realiza el consumo
            List<Rese> reses = await dbContext.Reses.Where(r => r.IdFinca == consumo.idFinca).ToListAsync();
            int nReses = reses.Count;

            //Si no hay reses, comunicar error
            if(nReses <= 0)
            {
                return "NoCows";
            }
            try
            {

                //Se recupera la entidad para modificar la cantidad de alimento de acuerdo al consumo
                Console.WriteLine(consumo.idFinca);
                Console.WriteLine(consumo.idAlimento);
                FincaAlimento registroConsumo = await
                    dbContext
                    .FincaAlimentos
                    .Where(fa =>
                        fa.IdFinca == consumo.idFinca &&
                        fa.IdAlimento == consumo.idAlimento &&
                        fa.NombreAlimento == consumo.NombreAlimento).FirstAsync();
                float ConsumoPorRes = consumo.Cantidad / nReses;
                foreach (Rese res in reses)
                {
                    await dbContext.HistorialAlimentacions.AddAsync(new HistorialAlimentacion()
                    {
                        IdAlimento = consumo.idAlimento,
                        IdFinca = consumo.idFinca,
                        IdRes = res.IdRes,
                        FechaAlimentacion = consumo.Fecha,
                        CantidadAlimentacion = ConsumoPorRes,
                        FechaCompra = registroConsumo.FechaCompra
                    });
                }
                await dbContext.SaveChangesAsync();
                if (registroConsumo.CantidadComprada < consumo.Cantidad)
                {
                    return "Cantidad Invalida";
                }
                if (registroConsumo != null)
                {
                    registroConsumo.CantidadComprada -= consumo.Cantidad;
                    dbContext.FincaAlimentos.Update(registroConsumo);
                }
                await dbContext.SaveChangesAsync();
                return "Agregado";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "ErrorBD";
            }
        }

        public async Task<List<FoodStateDto>> GetFoodByEstate(int IdFinca)
        {
            try
            {
                List<FoodStateDto> foodStates =  new List<FoodStateDto>();
                List<FincaAlimento> foodByState = await dbContext.FincaAlimentos.Where(a => a.IdFinca == IdFinca).ToListAsync();
                foreach(FincaAlimento food in foodByState)
                {
                    foodStates.Add(new FoodStateDto()
                    {
                        IdAlimento = food.IdAlimento,
                        NombreAlimento = food.NombreAlimento,
                        PrecioAlimento = food.PrecioAlimento,
                        FechaCompra = food.FechaCompra,
                        CantidadComprada = food.CantidadComprada,
                        IdFinca = food.IdFinca
                    });
                }
                return foodStates;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AddFoodToState(FoodStateDto foodStateDto)
        {

            try
            {
                FincaAlimento fincaAlimento = new()
                {
                    IdAlimento = foodStateDto.IdAlimento,
                    IdFinca = foodStateDto.IdFinca,
                    NombreAlimento = foodStateDto.NombreAlimento,
                    PrecioAlimento = foodStateDto.PrecioAlimento,
                    CantidadComprada = foodStateDto.CantidadComprada,
                    FechaCompra = foodStateDto.FechaCompra

                };

                dbContext.FincaAlimentos.Add(fincaAlimento);
                await dbContext.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> ModifyFood(FoodDto foodDto)
        {
            try
            {
                Alimento alimento = await dbContext.Alimentos.Where(a => a.IdAlimento == foodDto.IdAlimento).FirstOrDefaultAsync();
             
                if (alimento != null)
                {
                    alimento.TipoAlimento = foodDto.TipoAlimento;
                    dbContext.Alimentos.Update(alimento);
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

        public async Task<bool> ModifyFoodState(FoodStateDto foodStateDto)
        {
            try
            {
                FincaAlimento finca = await dbContext.FincaAlimentos.Where(a => a.IdAlimento == foodStateDto.IdAlimento && a.IdFinca == foodStateDto.IdFinca && a.FechaCompra == foodStateDto.FechaCompra).FirstOrDefaultAsync();
                if (finca != null)
                {
                    finca.NombreAlimento = foodStateDto.NombreAlimento;
                    finca.PrecioAlimento = foodStateDto.PrecioAlimento;
                    finca.CantidadComprada = finca.CantidadComprada;
                    dbContext.FincaAlimentos.Update(finca);
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

        public async Task<FoodDto> GetFood(int idFood)
        {
            try
            {
                Alimento alimentos = await dbContext.Alimentos.Where(x => x.IdAlimento == idFood).FirstOrDefaultAsync();
                if (alimentos != null)
                {
                    FoodDto food = new()
                    {
                        IdAlimento = alimentos.IdAlimento,
                        TipoAlimento = alimentos.TipoAlimento
                    };
                    return food;
                }
                return null;

            }
            catch
            {
                return null;
            }
           
        }
    }
}

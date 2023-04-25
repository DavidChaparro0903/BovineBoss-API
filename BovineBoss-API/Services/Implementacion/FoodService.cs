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

        public Task<bool> ModifyFoodState(FoodStateDto foodStateDto)
        {
            throw new NotImplementedException();
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

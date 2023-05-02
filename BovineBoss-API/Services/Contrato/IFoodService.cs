namespace BovineBoss_API.Services.Contrato;
using BovineBoss_API.Models.Dtos;


 public interface IFoodService
    {
        Task<List<FoodDto>> GetListFood();
    
        Task<bool> AddFood(CreateFoodDto food);

        Task<bool> AddFoodToState(FoodStateDto foodStateDto);


        Task<bool> ModifyFood(FoodDto foodDto);

        Task<bool> ModifyFoodState(FoodStateDto foodStateDto);

        Task<FoodDto> GetFood(int idFood);
        Task<List<FoodStateDto>> GetFoodByEstate(int IdFinca);
        Task<String> AddFoodConsumo(ConsumoDTO consumoDTO);
}



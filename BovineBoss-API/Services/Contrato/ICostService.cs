using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface ICostService
    {

        Task<List<CostCompleteDto>> GetListCost();

        Task<bool> AddCost(CostDto cost);

        Task<bool> ModifyCost(ModifyCost modify);

        Task<bool> AddCostToState(CostStateDto costStateDto);

        Task<bool> ModifyCostToState(ModifyCostStateDto costStateDto);

        Task<List<CostByStateDTO>> GetCostsByState(int idFinca);
    }
}

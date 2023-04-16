using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface IFincaService
    {
        Task<List<FincaDto>> GetList();

        Task<FincaDto> GetFinca(int idFinca);


        Task<bool> fincaExits(int idFinca);

        Task<CreateFincaDto> AddFinca(CreateFincaDto finca);

        Task<bool> UpdateFinca(Finca finca);

        Task<bool> DeleteFinca(Finca Finca);

        Task<List<StateTokenDto>> GetListState();
        Task<List<StateTokenDto>> GetListStateByIdUser(int userId);

    }
}

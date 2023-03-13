using BovineBoss_API.Models.DB;
using BovineBoss_API.Models.Dtos;

namespace BovineBoss_API.Services.Contrato
{
    public interface IFincaService
    {
        Task<List<FincasDto>> GetList();

        Task<FincasDto> GetFinca(int idFinca);

        Task<Finca> AddFinca(Finca finca);

        Task<bool> UpdateFinca(Finca finca);

        Task<bool> DeleteFinca(Finca Finca);

    }
}
